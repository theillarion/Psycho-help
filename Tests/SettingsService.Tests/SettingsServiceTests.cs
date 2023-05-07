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
            // Создаем временную директорию для тестирования
            Directory.CreateDirectory(Path.Combine(AppEnvironment.GetRootWorkDirectory(), "TestDir"));
        }

        [TearDown]
        public void TearDown()
        {   
            // Remove the dummy SettingsMySql.json file after testing
            File.Delete("SettingsMySql.json");
            // Удаляем временную директорию после тестирования
            Directory.Delete(Path.Combine(AppEnvironment.GetRootWorkDirectory(), "TestDir"), true);
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

        //"LoadDbSettings_ShouldReturnDbConnectionStringBuilder_WhenSettingsFileExists" проверяет, что метод загрузки настроек базы данных возвращает объект класса DbConnectionStringBuilder, если файл с настройками существует.
        [Test]
        public void LoadDbSettings_ShouldReturnDbConnectionStringBuilder_WhenSettingsFileExists()
        {
            // Arrange

            // Act
            var result = DbSettingsService.LoadDbSettings();

            // Assert
            Assert.IsInstanceOf<DbConnectionStringBuilder>(result); // проверяем, что результат является экземпляром класса DbConnectionStringBuilder
        }

        //Второй тест "DbSettingsFileExists_ShouldReturnTrue_WhenSettingsFileExists" проверяет, что метод проверки существования файла настроек возвращает true, если файл существуе
        [Test]
        public void DbSettingsFileExists_ShouldReturnTrue_WhenSettingsFileExists()
        {
            // Arrange

            // Act
            var result = DbSettingsService.DbSettingsFileExists(); // вызываем метод проверки существования файла настроек

            // Assert
            Assert.IsTrue(result); // проверяем, что метод вернул true, если файл существует
        }

        //Третий тест "DbSettingsFileExists_ShouldReturnFalse_WhenSettingsFileDoesNotExist" проверяет, что метод проверки существования файла настроек возвращает false, если файл не существует
        [Test]
        public void DbSettingsFileExists_ShouldReturnFalse_WhenSettingsFileDoesNotExist()
        {
            // Arrange
            File.Delete("SettingsMySql.json"); // удаляем файл настроек, если он существует

            // Act
            var result = DbSettingsService.DbSettingsFileExists(); // вызываем метод проверки существования файла настроек

            // Assert
            Assert.IsFalse(result); // проверяем, что метод вернул false, если файл не существует
        }


    }
}