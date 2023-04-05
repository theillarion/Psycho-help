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
            // Создаем временную директорию для тестирования
            Directory.CreateDirectory(Path.Combine(AppEnvironment.GetRootWorkDirectory(), "TestDir"));
        }

        [TearDown]
        public void TearDown()
        {
            // Удаляем временную директорию после тестирования
            Directory.Delete(Path.Combine(AppEnvironment.GetRootWorkDirectory(), "TestDir"), true);
        }

        //LoadDbSettings_WhenFileExists_ReturnsDbConnectionStringBuilder() - тест проверяет, что метод LoadDbSettings() правильно загружает настройки из файла, если файл существует. В этом тесте создается файл с настройками базы данных, после чего вызывается метод LoadDbSettings(), который должен вернуть объект типа MySqlConnectionStringBuilder с теми же настройками.
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

        //LoadDbSettings_WhenFileDoesNotExist_ReturnsEmptyDbConnectionStringBuilder() - тест проверяет, что метод LoadDbSettings() возвращает пустой объект типа MySqlConnectionStringBuilder, если файл с настройками базы данных не существует.
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

        //DbSettingsFileExists_WhenFileExists_ReturnsTrue() - тест проверяет, что метод DbSettingsFileExists() возвращает true, если файл с настройками базы данных существует.
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

        //DbSettingsFileExists_WhenFileDoesNotExist_ReturnsFalse() - тест проверяет, что метод DbSettingsFileExists() возвращает false, если файл с настройками базы данных не существует.
        [Test]
        public void DbSettingsFileExists_WhenFileDoesNotExist_ReturnsFalse()
        {
            // Arrange

            // Act
            var actual = DbSettingsService.DbSettingsFileExists();

            // Assert
            Assert.IsFalse(actual);
        }

        //RemoveDbSettings_WhenFileExists_RemovesFile() - тест проверяет, что метод RemoveDbSettings() удаляет файл с настройками базы данных, если файл существует.
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

        //RemoveDbSettings_WhenFileDoesNotExist_DoesNothing() - тест проверяет, что метод RemoveDbSettings() не делает ничего, если файл с настройками базы данных не существует.
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