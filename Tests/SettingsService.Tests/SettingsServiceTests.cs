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
    }
}