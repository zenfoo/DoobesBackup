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
            //var connection = new SqliteConnection("Data Source=:memory:");
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
            using (var connection = DbHelper.GetDbConnection())
            {
                var count = connection.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type='table' AND name=@TableName;", new { TableName = tableName });
                return count > 0;
            }
        }

        /// <summary>
        /// Create a table that maps to the type specified
        /// </summary>
        /// <param name="entityType">The object type to create</param>
        public static void CreateTable(string tableName, Type entityType)
        {
            var properties = entityType.GetProperties();
            var sb = new StringBuilder();
            sb.Append("CREATE TABLE " + tableName + "(");

            for (var ii = 0; ii < properties.Length; ii++)
            {
                var prop = properties[ii];
                
                // Map properties to fields
                if (prop.Name.ToLowerInvariant() == "id")
                {
                    // Add comma between each field declaration
                    if (ii > 0)
                    {
                        sb.Append(",");
                    }

                    // Expect id to be a guid
                    sb.Append("Id TEXT PRIMARY KEY");
                    
                } else {
                    // Detect the type of the property down to one of TEXT, NUMERIC, INTEGER, REAL
                    var fieldType = "";
                    var typeCode = Type.GetTypeCode(prop.PropertyType);
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
                            fieldType = "INTEGER";
                            break;
                        case TypeCode.Char:
                        case TypeCode.String:
                            fieldType = "TEXT";
                            break;
                        case TypeCode.Double:
                        case TypeCode.Single:
                            fieldType = "REAL";
                            break;
                        case TypeCode.Boolean:
                        case TypeCode.DateTime:
                        case TypeCode.Decimal:
                            fieldType = "NUMERIC";
                            break;
                        case TypeCode.Object:
                            // We will store the id of the entity in this field (Guid format)
                            if (typeof(PersistenceModel).IsAssignableFrom(prop.PropertyType))
                            {
                                fieldType = "TEXT";
                                name = prop.Name + "Id";
                            }
                            break;
                    }
                    
                    if (!string.IsNullOrEmpty(fieldType))
                    {
                        // Add comma between each field declaration
                        if (ii > 0)
                        {
                            sb.Append(",");
                        }

                        sb.Append(name + " " + fieldType);
                    }
                }
            }
            sb.Append(");");

            using (var connection = DbHelper.GetDbConnection())
            {
                var result = connection.Execute(sb.ToString());
            }
        }
     }
}
