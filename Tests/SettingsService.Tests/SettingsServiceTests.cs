using MySql.Data.MySqlClient;
using NUnit.Framework;
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
            // ������� ��������� ���������� ��� ������������
            Directory.CreateDirectory(Path.Combine(AppEnvironment.GetRootWorkDirectory(), "TestDir"));
        }

        [TearDown]
        public void TearDown()
        {
            // ������� ��������� ���������� ����� ������������
            Directory.Delete(Path.Combine(AppEnvironment.GetRootWorkDirectory(), "TestDir"), true);
        }

        //LoadDbSettings_WhenFileExists_ReturnsDbConnectionStringBuilder() - ���� ���������, ��� ����� LoadDbSettings() ��������� ��������� ��������� �� �����, ���� ���� ����������. � ���� ����� ��������� ���� � ����������� ���� ������, ����� ���� ���������� ����� LoadDbSettings(), ������� ������ ������� ������ ���� MySqlConnectionStringBuilder � ���� �� �����������.
        [Test]
        public void LoadDbSettings_WhenFileExists_ReturnsDbConnectionStringBuilder()
        {
            // Arrange
            var expectedConnectionString = "Server=localhost;Database=test;Uid=root;Pwd=pass;";
            var expected = new MySqlConnectionStringBuilder(expectedConnectionString);
            var filePath = Path.Combine(AppEnvironment.GetRootWorkDirectory(), "TestDir", "SettingsMySql.json");
            File.WriteAllText(filePath, expectedConnectionString);

            // Act
            var actual = DbSettingsService.LoadDbSettings();

            // Assert
            Assert.AreEqual(expected.ConnectionString, actual.ConnectionString);
        }

        //LoadDbSettings_WhenFileDoesNotExist_ReturnsEmptyDbConnectionStringBuilder() - ���� ���������, ��� ����� LoadDbSettings() ���������� ������ ������ ���� MySqlConnectionStringBuilder, ���� ���� � ����������� ���� ������ �� ����������.
        [Test]
        public void LoadDbSettings_WhenFileDoesNotExist_ReturnsEmptyDbConnectionStringBuilder()
        {
            // Arrange
            var expected = new MySqlConnectionStringBuilder();

            // Act
            var actual = DbSettingsService.LoadDbSettings();

            // Assert
            Assert.AreEqual(expected.ConnectionString, actual.ConnectionString);
        }

        //DbSettingsFileExists_WhenFileExists_ReturnsTrue() - ���� ���������, ��� ����� DbSettingsFileExists() ���������� true, ���� ���� � ����������� ���� ������ ����������.
        [Test]
        public void DbSettingsFileExists_WhenFileExists_ReturnsTrue()
        {
            // Arrange
            var filePath = Path.Combine(AppEnvironment.GetRootWorkDirectory(), "TestDir", "SettingsMySql.json");
            File.WriteAllText(filePath, "test");

            // Act
            var actual = DbSettingsService.DbSettingsFileExists();

            // Assert
            Assert.IsTrue(actual);
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

        //RemoveDbSettings_WhenFileExists_RemovesFile() - ���� ���������, ��� ����� RemoveDbSettings() ������� ���� � ����������� ���� ������, ���� ���� ����������.
        [Test]
        public void RemoveDbSettings_WhenFileExists_RemovesFile()
        {
            // Arrange
            var filePath = Path.Combine(AppEnvironment.GetRootWorkDirectory(), "TestDir", "SettingsMySql.json");
            File.WriteAllText(filePath, "test");

            // Act
            DbSettingsService.RemoveDbSettings();

            // Assert
            Assert.IsFalse(File.Exists(filePath));
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
    }
}