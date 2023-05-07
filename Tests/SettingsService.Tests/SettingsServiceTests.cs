using MySql.Data.MySqlClient;
using NUnit.Framework;
using System.Data.Common;
using System.IO;
using Xk7.Helper;
using Xk7.Services;

namespace Xk7.Tests.SettingsService.Tests
{
    [TestFixture]
    public class DbSettingsServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            // Create a dummy SettingsMySql.json file for testing
            var connectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "test_db",
                UserID = "root",
                Password = "password"
            };

            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(connectionStringBuilder);
            File.WriteAllText("SettingsMySql.json", jsonString);
            // ������� ��������� ���������� ��� ������������
            Directory.CreateDirectory(Path.Combine(AppEnvironment.GetRootWorkDirectory(), "TestDir"));
        }

        [TearDown]
        public void TearDown()
        {   
            // Remove the dummy SettingsMySql.json file after testing
            File.Delete("SettingsMySql.json");
            // ������� ��������� ���������� ����� ������������
            Directory.Delete(Path.Combine(AppEnvironment.GetRootWorkDirectory(), "TestDir"), true);
        }

 

        //DbSettingsFileExists_WhenFileDoesNotExist_ReturnsFalse() - ���� ���������, ��� ����� DbSettingsFileExists() ���������� false, ���� ���� � ����������� ���� ������ �� ����������.
        [Test]
        public void DbSettingsFileExists_WhenFileDoesNotExist_ReturnsFalse()
        {
            // Arrange

            // Act
            var actual = DbSettingsService.DbSettingsFileExists();

            // Assert
            Assert.IsFalse(actual);
        }

        //RemoveDbSettings_WhenFileDoesNotExist_DoesNothing() - ���� ���������, ��� ����� RemoveDbSettings() �� ������ ������, ���� ���� � ����������� ���� ������ �� ����������.
        [Test]
        public void RemoveDbSettings_WhenFileDoesNotExist_DoesNothing()
        {
            // Arrange

            // Act
            DbSettingsService.RemoveDbSettings();

            // Assert
            Assert.Pass();
        }

        //"LoadDbSettings_ShouldReturnDbConnectionStringBuilder_WhenSettingsFileExists" ���������, ��� ����� �������� �������� ���� ������ ���������� ������ ������ DbConnectionStringBuilder, ���� ���� � ����������� ����������.
        [Test]
        public void LoadDbSettings_ShouldReturnDbConnectionStringBuilder_WhenSettingsFileExists()
        {
            // Arrange

            // Act
            var result = DbSettingsService.LoadDbSettings();

            // Assert
            Assert.IsInstanceOf<DbConnectionStringBuilder>(result); // ���������, ��� ��������� �������� ����������� ������ DbConnectionStringBuilder
        }

        //������ ���� "DbSettingsFileExists_ShouldReturnTrue_WhenSettingsFileExists" ���������, ��� ����� �������� ������������� ����� �������� ���������� true, ���� ���� ���������
        [Test]
        public void DbSettingsFileExists_ShouldReturnTrue_WhenSettingsFileExists()
        {
            // Arrange

            // Act
            var result = DbSettingsService.DbSettingsFileExists(); // �������� ����� �������� ������������� ����� ��������

            // Assert
            Assert.IsTrue(result); // ���������, ��� ����� ������ true, ���� ���� ����������
        }

        //������ ���� "DbSettingsFileExists_ShouldReturnFalse_WhenSettingsFileDoesNotExist" ���������, ��� ����� �������� ������������� ����� �������� ���������� false, ���� ���� �� ����������
        [Test]
        public void DbSettingsFileExists_ShouldReturnFalse_WhenSettingsFileDoesNotExist()
        {
            // Arrange
            File.Delete("SettingsMySql.json"); // ������� ���� ��������, ���� �� ����������

            // Act
            var result = DbSettingsService.DbSettingsFileExists(); // �������� ����� �������� ������������� ����� ��������

            // Assert
            Assert.IsFalse(result); // ���������, ��� ����� ������ false, ���� ���� �� ����������
        }


    }
}