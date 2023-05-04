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
        //TestExistsUser_WhenUserExists_ShouldReturnTrue: проверяет, что метод ExistsUser возвращает true, если пользователь с заданным логином существует.
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

        //TestExistsUser_WhenUserDoesNotExist_ShouldReturnFalse: проверяет, что метод ExistsUser возвращает false, если пользователь с заданным логином не существует.
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

        //TestIsBannedUser_WhenUserIsBanned_ShouldReturnTrue: проверяет, что метод IsBannedUser возвращает true, если пользователь с заданным логином заблокирован.
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

        //TestIsBannedUser_WhenUserIsNotBanned_ShouldReturnFalse: проверяет, что метод IsBannedUser возвращает false, если пользователь с заданным логином не заблокирован.
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

        //TestGetHashPassword_WhenUserExists_ShouldReturnHashPassword: проверяет, что метод GetHashPassword возвращает правильный хэш-пароль для заданного логина.
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


        //TestGetDataUserByLogin_WhenUserExists_ShouldReturnDataRow: проверяет, что метод GetDataUserByLogin возвращает DataRow, содержащий информацию о пользователе с заданным логином.
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

        //TestGetDataUserByLogin_WhenUserDoesNotExist_ShouldReturnNull: проверяет, что метод GetDataUserByLogin возвращает null, если пользователь с заданным логином не существует в базе данных.
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
        //TestAddUser_WhenUserIsAdded_ShouldReturnAddUserResultSuccess: проверяет, что метод AddUser возвращает AddUserResult.Success, если пользователь успешно добавлен в базу данных.
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

        //TestAddUser_WhenUserAlreadyExists_ShouldReturnAddUserResultUserAlreadyExists: проверяет, что метод AddUser возвращает AddUserResult.UserAlreadyExists, если пользователь с таким логином уже существует в базе данных.
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

        //TestAddUser_WhenDatabaseErrorOccurs_ShouldReturnAddUserResultDatabaseError: проверяет, что метод AddUser возвращает AddUserResult.DatabaseError, если произошла ошибка при добавлении пользователя в базу данных.
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