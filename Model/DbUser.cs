using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using App.Helper;
using App.Helper.Converts;

namespace App.Model
{
    internal class DbUser : IDbUser
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
    }
}
