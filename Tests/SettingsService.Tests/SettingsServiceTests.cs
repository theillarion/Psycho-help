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
            // —оздаем временную директорию дл€ тестировани€
            Directory.CreateDirectory(Path.Combine(AppEnvironment.GetRootWorkDirectory(), "TestDir"));
        }

        [TearDown]
        public void TearDown()
        {
            // ”дал€ем временную директорию после тестировани€
            Directory.Delete(Path.Combine(AppEnvironment.GetRootWorkDirectory(), "TestDir"), true);
        }

 

        //DbSettingsFileExists_WhenFileDoesNotExist_ReturnsFalse() - тест провер€ет, что метод DbSettingsFileExists() возвращает false, если файл с настройками базы данных не существует.
        [Test]
        public void DbSettingsFileExists_WhenFileDoesNotExist_ReturnsFalse()
        {
            // Arrange

            // Act
            var actual = DbSettingsService.DbSettingsFileExists();

            // Assert
            Assert.IsFalse(actual);
        }

        //RemoveDbSettings_WhenFileDoesNotExist_DoesNothing() - тест провер€ет, что метод RemoveDbSettings() не делает ничего, если файл с настройками базы данных не существует.
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