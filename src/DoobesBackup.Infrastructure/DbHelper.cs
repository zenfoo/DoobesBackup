//-----------------------------------------------------------------------
// <copyright file="DbHelper.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Infrastructure
{
    using Dapper;
    using Microsoft.Data.Sqlite;
    using PersistenceModels;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Text;

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
        public static IDbConnection GetDbConnection()
        {
            var connection = new SqliteConnection("Data Source=data.db");
            connection.Open();
            return connection;
        }
        
        /// <summary>
        /// Determine if a table exists in the data store
        /// </summary>
        /// <param name="tableName">The name of the table</param>
        /// <returns>Boolean value indicating whether the table exists or not</returns>
        public static bool TableExists(string tableName)
        {
            using (var db = DbHelper.GetDbConnection())
            {
                var count = db.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type='table' AND name=@TableName;", new { TableName = tableName });
                return count > 0;
            }
        }

        /// <summary>
        /// Create a table that maps to the type specified
        /// </summary>
        /// <param name="entityType">The object type to create</param>
        public static void CreateTable(string tableName, Type entityType)
        {
            var columns = DbHelper.GetColumnsForType(entityType);
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
            using (var db = DbHelper.GetDbConnection())
            {
                var result = db.Execute(sb.ToString());
            }
        }

        public static IEnumerable<Column> GetColumnsForType<T>() where T : PersistenceModel
        {
            return DbHelper.GetColumnsForType(typeof(T));
        }

        public static IEnumerable<Column> GetColumnsForType(Type entityType)
        {
            var columns = new Collection<Column>();
            var properties = entityType.GetProperties();
            for (var ii = 0; ii < properties.Length; ii++)
            {
                var prop = properties[ii];

                // Map properties to fields
                if (prop.Name.ToLowerInvariant() == "id")
                {
                    columns.Add(new Column()
                    {
                        Name = "Id",
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
                    var typeCode = Type.GetTypeCode(prop.PropertyType);
                    var foreignKey = false;
                    var propertyPath = prop.Name;
                    var name = prop.Name;
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
                            if (typeof(PersistenceModel).IsAssignableFrom(prop.PropertyType) &&
                                DbHelper.IsOwner(prop))
                            {
                                fieldType = ColumnType.TEXT;
                                foreignKey = true;
                                name = prop.Name + "Id";
                                propertyPath = prop.Name + ".Id";
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
        private static bool IsOwner(PropertyInfo prop)
        {
            var relationshipAttribute = prop.GetCustomAttribute<RelationshipAttribute>();
            if (relationshipAttribute != null)
            {
                return relationshipAttribute.IsOwner;
            }

            // By default we assume this is the owning side of the relationship
            return true;
        }
    }
}
