using EfCore_CodeFirstPracticePersistenceLayer;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace EFCore_CodeFirstPracticeUnitTest
{
    public class Tests
    {

        OrderDbContext orderDbContext;
        Assembly persistenceLayer;

        [SetUp]
        public void Setup()
        {
            orderDbContext = new OrderDbContext();
            persistenceLayer = typeof(OrderDbContext).Assembly;

            orderDbContext.Database.EnsureDeleted();
            orderDbContext.Database.EnsureCreated();

        }
        [Test]
        public void FirstMigrationExist()
        {
            var result = persistenceLayer.GetTypes().FirstOrDefault(x => x.Name.Contains("InitialCreation"));
            Assert.IsNotNull(result);
        }
        [Test]
        public void MigrationStep3Exist()
        {
            var result = persistenceLayer.GetTypes().FirstOrDefault(x => x.Name.Contains("ChangeTableAndFieldName"));
            Assert.IsNotNull(result);
        }
        [Test]
        public void MigrationStep4Exist()
        {
            var result = persistenceLayer.GetTypes().FirstOrDefault(x => x.Name.Contains("ChangeMaxLength"));
            Assert.IsNotNull(result);
        }
        [Test]
        public void MigrationStep5Exist()
        {
            var result = persistenceLayer.GetTypes().FirstOrDefault(x => x.Name.Contains("ChangeFKName"));
            Assert.IsNotNull(result);
        }

        [Test]
        public void CustumerOrderTableExist()
        {
            TableExist("CustomerWithOrder");
        }

        [Test]
        public void ChangeCustomerTableNameAndChangeFirstNameTest()
        {
            TableExist("CustomerWithOrder");
            ColumnExist("CustomerWithOrder", "CustomerFirstName");
        }

        [Test]
        public void FirstNameMaxLengthTest()
        {
            TableExist("CustomerWithOrder");
            ColumnExist("CustomerWithOrder", "CustomerFirstName");
            MaxLengthCheck("CustomerWithOrder", "CustomerFirstName", 50);
        }

        [Test]
        public void ChangeFKNameTest()
        {
            TableExist("Orders");
            ColumnExist("Orders", "FK_CustomerID");
        }

        public void TableExist(string tableName)
        {
            using(SqlConnection con = CreateSQLConnection())
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(BuildSqlForTestTableExist(tableName), con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Assert.AreEqual(1, reader.GetInt32(0));
                        }
                    }
                }
            }
        }

        public void ColumnExist(string tableName, string columnName)
        {
            using (SqlConnection con = CreateSQLConnection())
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(BuildSqlForTestColumnName(tableName, columnName), con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Assert.AreEqual(1, reader.GetInt32(0));
                        }
                    }
                }

            }
        }
        public void MaxLengthCheck(string tableName, string columnName, int length)
        {
            using (SqlConnection con = CreateSQLConnection())
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(BuildSQLForTestColumnMaxLength(tableName, columnName), con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Assert.AreEqual(length, reader.GetInt32(0));
                        }
                    }
                }

            }
        }


        public string BuildSqlForTestColumnName(string tableName, string columnName)
        {
            return @$" IF EXISTS(SELECT 1 FROM sys.columns
                     WHERE Name = N'{columnName}'
                     AND Object_ID = Object_ID(N'dbo.{tableName}'))
                    SELECT 1 AS res ELSE SELECT 0 AS res;";
        }

        public string BuildSqlForTestTableExist(string tableName)
        {
            return @$"IF EXISTS (SELECT 1 
           FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_TYPE='BASE TABLE' 
           AND TABLE_NAME='{tableName}') 
   SELECT 1 AS res ELSE SELECT 0 AS res;
        ";
        }

        public string BuildSQLForTestColumnMaxLength(string tableName, string columnName)
        {
            return @$"SELECT CHARACTER_MAXIMUM_LENGTH 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE 
     TABLE_NAME = '{tableName}' AND 
     COLUMN_NAME = '{columnName}'";
        }

        public SqlConnection CreateSQLConnection()
        {
            return new SqlConnection(ConfigurationConst.ConnectionString);
        }
      

      

        [Test]
        public void FieldType()
        {
            Assert.Pass();
        }

        [TearDown]
        public void TearsDown()
        {
            orderDbContext.Database.ExecuteSqlRaw(@"while(exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_TYPE='FOREIGN KEY'))
begin
 declare @sql nvarchar(2000)
 SELECT TOP 1 @sql=('ALTER TABLE ' + TABLE_SCHEMA + '.[' + TABLE_NAME
 + '] DROP CONSTRAINT [' + CONSTRAINT_NAME + ']')
 FROM information_schema.table_constraints
 WHERE CONSTRAINT_TYPE = 'FOREIGN KEY'
 exec (@sql)
 PRINT @sql
end


while(exists(select 1 from INFORMATION_SCHEMA.TABLES 
             where TABLE_NAME != '__MigrationHistory' 
             AND TABLE_TYPE = 'BASE TABLE'))
begin
 
 SELECT TOP 1 @sql=('DROP TABLE ' + TABLE_SCHEMA + '.[' + TABLE_NAME
 + ']')
 FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME != '__MigrationHistory' AND TABLE_TYPE = 'BASE TABLE'
exec (@sql)
 /* you dont need this line, it just shows what was executed */
 PRINT @sql
end");
        }
    }
}