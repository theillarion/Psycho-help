using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xk7.Helper;
using Xk7.Helper.Converts;

namespace Xk7.Model
{
    internal class DbUser : IDbUser, ICloneable
    {
        public uint IdUserRole { get; set; }
        public string Login { get; set; }
        public byte[] HashPassword { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime DateBirthday { get; set; }
        public bool IsBlocked { get; set; }
        public DbUser() {}
        public DbUser(uint idUserRole, string login, byte[] hashPassword, string firstName, string secondName,
            DateTime dateBirthday, bool isBlocked)
        {
            IdUserRole = idUserRole;
            Login = login;
            HashPassword = hashPassword;
            FirstName = firstName;
            SecondName = secondName;
            DateBirthday = dateBirthday;
            IsBlocked = isBlocked;
        }
        public override string ToString()
        {
            return
                $"[ IdUserRole: {IdUserRole.ToString()}, Login: {Login}, HashPassword: {Converts.ConvertByteArrayToString(HashPassword)}, FirstName: {FirstName}, " +
                $"SecondName: {SecondName}, DateBirthday: {DateBirthday}, IsBlocked: {IsBlocked}]";
        }

        public static bool operator ==(DbUser user1, DbUser user2)
        {
            return user1.IdUserRole == user2.IdUserRole && user1.IsBlocked == user2.IsBlocked && user1.Login == user2.Login
                && user1.HashPassword == user2.HashPassword && user1.FirstName == user2.FirstName && user1.SecondName == user2.SecondName
                && user1.DateBirthday == user2.DateBirthday;
        }

        public static bool operator !=(DbUser user1, DbUser user2)
        {
            return !(user1.IdUserRole == user2.IdUserRole && user1.IsBlocked == user2.IsBlocked && user1.Login == user2.Login
                && user1.HashPassword == user2.HashPassword && user1.FirstName == user2.FirstName && user1.SecondName == user2.SecondName
                && user1.DateBirthday == user2.DateBirthday);
        }

        public object Clone()
        {
            return new DbUser(IdUserRole, Login, HashPassword, FirstName, SecondName, DateBirthday, IsBlocked);
        }

    }
}
