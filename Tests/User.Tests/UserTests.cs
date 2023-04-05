using NUnit.Framework;
using System;
using Xk7.Helper.Enums;
using Xk7.Model;

namespace xk7.Tests
{
    [TestFixture]
    public class UserTests
    {
        //Constructor_WithValidArguments_SetsProperties(): проверяет, что свойства объекта User устанавливаются правильно при вызове конструктора с правильными аргументами. Создаются переменные с различными значениями для свойств, и затем объект User создается с использованием этих значений. После этого проверяется, что все свойства объекта User установлены на правильные значения.
        [Test]
        public void Constructor_WithValidArguments_SetsProperties()
        {
            // Arrange
            UserRole userRole = UserRole.Admin;
            string login = "john_doe";
            HashedValue password = new HashedValue("mypassword");
            string firstName = "John";
            string secondName = "Doe";
            DateOnly dateBirthday = new DateOnly(1990, 5, 1);
            bool isBanned = false;

            // Act
            var user = new User(userRole, login, password, firstName, secondName, dateBirthday, isBanned);

            // Assert
            Assert.AreEqual(userRole, user.IdUserRole);
            Assert.AreEqual(login, user.Login);
            Assert.AreEqual(password, user.Password);
            Assert.AreEqual(firstName, user.FirstName);
            Assert.AreEqual(secondName, user.SecondName);
            Assert.AreEqual(dateBirthday, user.DateBirthday);
            Assert.AreEqual(isBanned, user.IsBanned);
        }

        //ToString_ReturnsExpectedString(): проверяет, что метод ToString() возвращает ожидаемую строку для объекта User. Создается объект User с некоторыми значениями свойств, и ожидаемая строка создается вручную, используя эти значения. Затем вызывается метод ToString() на объекте User, и полученная строка сравнивается с ожидаемой строкой.
        [Test]
        public void ToString_ReturnsExpectedString()
        {
            // Arrange
            var user = new User(UserRole.User, "jane_doe", new HashedValue("mypassword"), "Jane", "Doe", new DateOnly(1995, 3, 15), true);
            string expectedString = "[ IdUserRole: User, Login: jane_doe, Password: " + user.Password.ToString() + ", FirstName: Jane, SecondName: Doe, DateBirthday: 3/15/1995, IsBanned: True]";

            // Act
            string resultString = user.ToString();

            // Assert
            Assert.AreEqual(expectedString, resultString);
        }
    }
}
