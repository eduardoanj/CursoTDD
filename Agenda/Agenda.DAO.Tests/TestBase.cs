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
            
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            
        }

        private void CreateDBTest()
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
            }
            
        }
        
    }
}