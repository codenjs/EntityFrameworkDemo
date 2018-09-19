using System;
using System.Data.SqlClient;
using System.IO;

namespace Tests.Framework
{
    public class DbTestUtilities
    {
        private string _connectionString;
        private string _masterConnectionString;

        public DbTestUtilities(string connectionString)
        {
            _connectionString = connectionString;
            _masterConnectionString = BuildMasterConnectionString();
        }

        public void CreateDatabaseIfNotExists()
        {
            var databaseName = new SqlConnectionStringBuilder(_connectionString).InitialCatalog;

            if (DatabaseExists(databaseName))
                return;

            CreateDatabase(databaseName);
            CreateSchema();
        }

        public string BuildMasterConnectionString()
        {
            var builder = new SqlConnectionStringBuilder(_connectionString);
            builder.InitialCatalog = "master";
            return builder.ToString();
        }

        public bool DatabaseExists(string databaseName)
        {
            var sql = $"SELECT database_id FROM sys.databases WHERE Name = '{databaseName}'";
            var id = ExecuteScalar<int?>(_masterConnectionString, sql);
            return id.HasValue;
        }

        public void CreateDatabase(string databaseName)
        {
            var sql = $"CREATE DATABASE [{databaseName}]";
            ExecuteNonQuery(_masterConnectionString, sql);
        }

        public void CreateSchema()
        {
            var fileName = "schema.sql";
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            var sql = File.ReadAllText(filePath);
            ExecuteNonQuery(sql);
        }

        public void ResetIdentitySeed(string table)
        {
            // This ensures the next identity value will be 1
            // Reseed command works differently if the identity value has not been used yet
            // If the current identity value is NULL and it's reset to 0, the next identity value will be 0
            // If the current identity value is not NULL and it's reset to 0, the next identity value will be 1
            // https://stackoverflow.com/questions/472578/dbcc-checkident-sets-identity-to-0

            var sql = $@"
                IF EXISTS (SELECT * FROM sys.identity_columns WHERE object_id = OBJECT_ID('{table}') AND last_value IS NOT NULL)
                    DBCC CHECKIDENT ([{table}], RESEED, 0)
                ";
            ExecuteNonQuery(sql);
        }

        public void ExecuteNonQuery(string sql)
        {
            ExecuteNonQuery(_connectionString, sql);
        }

        public void ExecuteNonQuery(string connectionString, string sql)
        {
            ExecuteCommand(connectionString, sql, cmd =>
            {
                cmd.ExecuteNonQuery();
                return 0;
            });
        }

        public T ExecuteScalar<T>(string sql)
        {
            return ExecuteScalar<T>(_connectionString, sql);
        }

        public T ExecuteScalar<T>(string connectionString, string sql)
        {
            return ExecuteCommand(connectionString, sql, cmd =>
            {
                var result = cmd.ExecuteScalar();
                return (T)result;
            });
        }

        private T ExecuteCommand<T>(string connectionString, string sql, Func<SqlCommand, T> commandFunc)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = sql;
                    return commandFunc(cmd);
                }
            }
        }
    }
}
