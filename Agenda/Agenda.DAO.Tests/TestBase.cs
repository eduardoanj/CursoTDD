using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using NUnit.Framework; 

namespace Agenda.DAL.Tests
{
    [TestFixture]
    public class TestBase
    {
        private string _script;
        private string _con;
        private string _catalogTest;

        public TestBase()
        {
            _script = @"DBAgendaTest_Create.sql";
            _con = ConfigurationManager.ConnectionStrings["conSetUpTest"].ConnectionString;
            _catalogTest = ConfigurationManager.ConnectionStrings["conSetUpTest"].ProviderName;
        }
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            CreateDbTest();
            
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            DeleteDbTest();
        }

        private void CreateDbTest()
        {
            using (var connection = new SqlConnection(_con))
            {
                connection.Open();
                var scriptSql = File
                    .ReadAllText($@"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}\{_script}")
                    .Replace("$(DefaultDataPath)", $@"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}")
                    .Replace("$(DefaultLogPath)", $@"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}")
                    .Replace("$(DefaultFilePrefix)", _catalogTest)
                    .Replace("$(DatabaseName)", _catalogTest)
                    .Replace("WITH (DATA_COMPRESSION = PAGE)", string.Empty)
                    .Replace("SET NOEXEC ON", string.Empty)
                    .Replace("GO\r\n", "|");
                ExecuteScriptSql(connection, scriptSql);
            }
        }

        private static void ExecuteScriptSql(SqlConnection connection, string scriptSql)
        {
            using (var command = connection.CreateCommand())
            {
                foreach (var sql in scriptSql.Split('|'))
                {
                    command.CommandText = sql;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(sql);
                        Console.WriteLine(e.Message);
                    }

                }
            }
        }
        
        private void DeleteDbTest()
        {
            using (var connection = new SqlConnection(_con))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@"USE [master]; 
                                            DECLARE @kill varchar(8000) = ''; 
                                            SELECT @kill = @kill + 'kill' + CONVERT(varchar(5), session_id) + ';' 
                                            FROM sys.dm_exec_sessions 
                                            WHERE database_id = db_id('{_catalogTest}') 
                                            EXEC(@kill);";
                    command.ExecuteNonQuery();
                    command.CommandText = $"DROP DATABASE {_catalogTest}";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}