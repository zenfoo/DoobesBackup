//-----------------------------------------------------------------------
// <copyright file="DbHelper.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Infrastructure
{
    using Dapper;
    using Extensions;
    using Microsoft.Data.Sqlite;
    using PersistenceModels;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Diagnostics;
    using System.IO;
    using Framework;

    /// <summary>
    /// Helper class for dealing with SqLite database implementation
    /// </summary>
    public static class DbHelper
    {
        /// <summary>
        /// Create and return a new SqLite database connection
        /// </summary>
        /// <param name="open">Open the connection immediately?</param>
        /// <returns>The database connection</returns>
        public static IDbConnection GetDbConnection(string dbName = "data.db")
        {
            var path = DbHelper.GetDbPath(dbName);
            var connection = new SqliteConnection(string.Format("Data Source={0}", path));
            connection.Open();
            return connection;
        }

        public static void DeleteDb(string dbName = "data.db")
        {
            if (File.Exists(GetDbPath(dbName)))
            {
                File.Delete(GetDbPath(dbName));
            }
        }
        
        /// <summary>
        /// Determine if a table exists in the data store
        /// </summary>
        /// <param name="tableName">The name of the table</param>
        /// <returns>Boolean value indicating whether the table exists or not</returns>
        public static bool TableExists(string tableName, string dbName = "data.db")
        {
            using (var db = DbHelper.GetDbConnection(dbName))
            {
                var count = db.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type='table' AND name=@TableName;", new { TableName = tableName });
                return count > 0;
            }
        }

        /// <summary>
        /// Create a table that maps to the type specified
        /// </summary>
        /// <param name="entityType">The object type to create</param>
        public static void CreateTable(string tableName, Type entityType, string dbName = "data.db")
        {
            var columns = DbHelper.GetColumnsForType(entityType);
            if (columns.Count() == 0)
            {
                throw new InvalidOperationException("The specified entity does not have any valid properties to persist!");
            }

            var sb = new StringBuilder();
            sb.Append("CREATE TABLE " + tableName + "(");
            var ii = 0;
            foreach (var column in columns)
            {
                sb.Append(
                    string.Format("{0} {1} {2} {3}",
                    ii > 0 ? "," : string.Empty,
                    column.Name,
                    column.Type.ToString(),
                    column.IsPrimaryKey ? "PRIMARY KEY" : string.Empty));
                ii++;
            }

            sb.Append(");");
            using (var db = DbHelper.GetDbConnection(dbName))
            {
                var result = db.Execute(sb.ToString());
            }
        }

        public static IEnumerable<Column> GetColumnsForType<T>() where T : PersistenceModel
        {
            return DbHelper.GetColumnsForType(typeof(T));
        }

        /// <summary>
        /// Retrieve a collection of columns for the given type
        /// </summary>
        /// <param name="entityType">The type to inspect (it is expected to be of type PersistenceModel)</param>
        /// <returns></returns>
        public static IEnumerable<Column> GetColumnsForType(Type entityType)
        {
            if (!typeof(PersistenceModel).IsAssignableFrom(entityType))
            {
                throw new ArgumentException("Invalid type specified, expecting a type of PersistenceModel", "entityType");
            }

            var columns = new Collection<Column>();

            // Sort the properties so the id property is listed first
            var properties = entityType.GetProperties()
                .OrderBy(p => p.Name, Comparer<string>.Create((x,y) => 
                {
                    // Ensure id is at the front of the list
                    if (x.ToLowerInvariant() == "id")
                    {
                        return -1;
                    }

                    if (y.ToLowerInvariant() == "id")
                    {
                        return 1;
                    }

                    return x.CompareTo(y);
                }));


            foreach(var member in properties)
            {
                // Map properties to fields
                if (member.Name.ToLowerInvariant() == "id")
                {
                    columns.Add(new Column()
                    {
                        Name = "_Id", // This is the name of the backing variable of the id in the PersistenceModel
                        Type = ColumnType.TEXT,
                        IsPrimaryKey = true,
                        IsForeignKey = false,
                        PropertyPath = "Id"
                    });
                }
                else
                {
                    // Detect the type of the property down to one of TEXT, NUMERIC, INTEGER, REAL
                    var fieldType = ColumnType.UNKNOWN;
                    var typeCode = Type.GetTypeCode(member.GetUnderlyingType());
                    var foreignKey = false;
                    var propertyPath = member.Name;
                    var name = member.Name;
                    switch (typeCode)
                    {
                        case TypeCode.Byte:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.SByte:
                        case TypeCode.UInt16:
                        case TypeCode.UInt32:
                        case TypeCode.UInt64:
                            fieldType = ColumnType.INTEGER;
                            break;
                        case TypeCode.Char:
                        case TypeCode.String:
                            fieldType = ColumnType.TEXT;
                            break;
                        case TypeCode.Double:
                        case TypeCode.Single:
                            fieldType = ColumnType.REAL;
                            break;
                        case TypeCode.Boolean:
                        case TypeCode.DateTime:
                        case TypeCode.Decimal:
                            fieldType = ColumnType.NUMERIC;
                            break;
                        case TypeCode.Object:
                            // We will store the id of the related entity in this field (Guid format)
                            // but only if this is not the aggregate root child entity and not the aggregate root object
                            if (typeof(PersistenceModel).IsAssignableFrom(member.GetUnderlyingType()) &&
                                DbHelper.IsOwner(member))
                            {
                                fieldType = ColumnType.TEXT;
                                foreignKey = true;
                                name = member.Name + "Id";
                                propertyPath = member.Name + ".Id";
                            }
                            break;
                    }

                    if (fieldType != ColumnType.UNKNOWN)
                    {
                        columns.Add(new Column()
                        {
                            Name = name,
                            Type = fieldType,
                            IsPrimaryKey = false,
                            IsForeignKey = foreignKey,
                            PropertyPath = propertyPath
                        });
                    }
                }
            }
            
            return columns;
        }

        /// <summary>
        /// Determine if this property is the owning side of the relationship
        /// </summary>
        /// <param name="propInfo"></param>
        /// <returns></returns>
        private static bool IsOwner(MemberInfo member)
        {
            var relationshipAttribute = member.GetCustomAttribute<RelationshipAttribute>();
            if (relationshipAttribute != null)
            {
                return relationshipAttribute.IsOwner;
            }

            // By default we assume this is the owning side of the relationship
            return true;
        }

        private static string GetDbPath(string dbName)
        {
            var directory = Path.GetDirectoryName(AssemblyHelper.GetAssemblyPathForType(typeof(DbHelper)));
            return Path.Combine(directory, dbName);
        }
    }
}
