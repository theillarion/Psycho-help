using NUnit.Framework;
using Moq;
using System.Data;
using Xk7.Services;
using Xk7.Helper.Enums;
using Xk7.Model;
using System;

namespace Xk7.Tests.IDbAsyncService.Tests
{
    [TestFixture]
    public class DbServiceTests
    {
        private Mock<IDbService> mockDbService;

        [SetUp]
        public void Setup()
        {
            mockDbService = new Mock<IDbService>();
        }
        //TestExistsUser_WhenUserExists_ShouldReturnTrue: ���������, ��� ����� ExistsUser ���������� true, ���� ������������ � �������� ������� ����������.
        [Test]
        public void TestExistsUser_WhenUserExists_ShouldReturnTrue()
        {
            // Arrange
            string login = "test_user";
            mockDbService.Setup(x => x.ExistsUser(login)).Returns(true);

            // Act
            bool result = mockDbService.Object.ExistsUser(login);

            // Assert
            Assert.IsTrue(result);
        }

        //TestExistsUser_WhenUserDoesNotExist_ShouldReturnFalse: ���������, ��� ����� ExistsUser ���������� false, ���� ������������ � �������� ������� �� ����������.
        [Test]
        public void TestExistsUser_WhenUserDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            string login = "test_user";
            mockDbService.Setup(x => x.ExistsUser(login)).Returns(false);

            // Act
            bool result = mockDbService.Object.ExistsUser(login);

            // Assert
            Assert.IsFalse(result);
        }

        //TestIsBannedUser_WhenUserIsBanned_ShouldReturnTrue: ���������, ��� ����� IsBannedUser ���������� true, ���� ������������ � �������� ������� ������������.
        [Test]
        public void TestIsBannedUser_WhenUserIsBanned_ShouldReturnTrue()
        {
            // Arrange
            string login = "banned_user";
            mockDbService.Setup(x => x.IsBannedUser(login)).Returns(true);

            // Act
            bool result = mockDbService.Object.IsBannedUser(login);

            // Assert
            Assert.IsTrue(result);
        }

        //TestIsBannedUser_WhenUserIsNotBanned_ShouldReturnFalse: ���������, ��� ����� IsBannedUser ���������� false, ���� ������������ � �������� ������� �� ������������.
        [Test]
        public void TestIsBannedUser_WhenUserIsNotBanned_ShouldReturnFalse()
        {
            // Arrange
            string login = "test_user";
            mockDbService.Setup(x => x.IsBannedUser(login)).Returns(false);

            // Act
            bool result = mockDbService.Object.IsBannedUser(login);

            // Assert
            Assert.IsFalse(result);
        }

        //TestGetHashPassword_WhenUserExists_ShouldReturnHashPassword: ���������, ��� ����� GetHashPassword ���������� ���������� ���-������ ��� ��������� ������.
        [Test]
        public void TestGetHashPassword_WhenUserExists_ShouldReturnHashPassword()
        {
            // Arrange
            string login = "test_user";
            string hashPassword = "test_password_hash";
            mockDbService.Setup(x => x.GetHashPassword(login)).Returns(hashPassword);

            // Act
            string result = mockDbService.Object.GetHashPassword(login);

            // Assert
            Assert.AreEqual(hashPassword, result);
        }


        //TestGetDataUserByLogin_WhenUserExists_ShouldReturnDataRow: ���������, ��� ����� GetDataUserByLogin ���������� DataRow, ���������� ���������� � ������������ � �������� �������.
        [Test]
        public void TestGetDataUserByLogin_WhenUserExists_ShouldReturnDataRow()
        {
            // Arrange
            string login = "test_user";
            DataRow expectedDataRow = new DataTable().NewRow();
            mockDbService.Setup(x => x.GetDataUserByLogin(login)).Returns(expectedDataRow);

            // Act
            DataRow? result = mockDbService.Object.GetDataUserByLogin(login);

            // Assert
            Assert.AreEqual(expectedDataRow, result);
        }

        //TestGetDataUserByLogin_WhenUserDoesNotExist_ShouldReturnNull: ���������, ��� ����� GetDataUserByLogin ���������� null, ���� ������������ � �������� ������� �� ���������� � ���� ������.
        [Test]
        public void TestGetDataUserByLogin_WhenUserDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            string login = "test_user";
            mockDbService.Setup(x => x.GetDataUserByLogin(login)).Returns((DataRow?)null);

            // Act
            DataRow? result = mockDbService.Object.GetDataUserByLogin(login);

            // Assert
            Assert.IsNull(result);
        }
        //TestAddUser_WhenUserIsAdded_ShouldReturnAddUserResultSuccess: ���������, ��� ����� AddUser ���������� AddUserResult.Success, ���� ������������ ������� �������� � ���� ������.
        [Test]
        public void TestAddUser_WhenUserIsAdded_ShouldReturnAddUserResultSuccess()
        {
            // Arrange
            User user = new User("test_user", "test_password");
            mockDbService.Setup(x => x.AddUser(user)).Returns(AddUserResult.Success);

            // Act
            AddUserResult result = mockDbService.Object.AddUser(user);

            // Assert
            Assert.AreEqual(AddUserResult.Success, result);
        }

        //TestAddUser_WhenUserAlreadyExists_ShouldReturnAddUserResultUserAlreadyExists: ���������, ��� ����� AddUser ���������� AddUserResult.UserAlreadyExists, ���� ������������ � ����� ������� ��� ���������� � ���� ������.
        [Test]
        public void TestAddUser_WhenUserAlreadyExists_ShouldReturnAddUserResultUserAlreadyExists()
        {
            // Arrange
            User user = new User("existing_user", "test_password");
            Moq.Language.Flow.IReturnsResult<IDbService> returnsResult = mockDbService.Setup(x => x.AddUser(user)).Returns(AddUserResult.UserAlreadyExists);

            // Act
            AddUserResult result = mockDbService.Object.AddUser(user);

            // Assert
            Assert.AreEqual(AddUserResult.UserAlreadyExists, result);
        }

        //TestAddUser_WhenDatabaseErrorOccurs_ShouldReturnAddUserResultDatabaseError: ���������, ��� ����� AddUser ���������� AddUserResult.DatabaseError, ���� ��������� ������ ��� ���������� ������������ � ���� ������.
        [Test]
        public void TestAddUser_WhenDatabaseErrorOccurs_ShouldReturnAddUserResultDatabaseError()
        {
            // Arrange
            User user = new User("test_user", "test_password");
            Moq.Language.Flow.IReturnsResult<IDbService> returnsResult = mockDbService.Setup(x => x.AddUser(user)).Returns(AddUserResult.DatabaseError);

            // Act
            AddUserResult result = mockDbService.Object.AddUser(user);

            // Assert
            Assert.AreEqual(AddUserResult.DatabaseError, result);
        }
       
    }
}