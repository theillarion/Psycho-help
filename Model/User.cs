using Xk7.Helper.Enums;
using System;

namespace Xk7.Model
{
    public class User
    {
        public UserRole IdUserRole;
        private string v1;
        private string v2;

        public string Login { get; set; }
        public HashedValue Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateOnly DateBirthday { get; set; }
        public bool IsBanned { get; set; } = false;
        public User() { }
        public User(UserRole userRole, string login, HashedValue password, string firstName, string secondName, DateOnly dateBirthday, bool isBanned)
        {
            IdUserRole = userRole;
            Login = login;
            Password = password;
            FirstName = firstName;
            SecondName = secondName;
            //DateBirthday = DateOnly.Parse(dateBirthday.ToString("yyyy'-'MM'-'dd"));
            DateBirthday = dateBirthday;
            IsBanned = isBanned;
        }

        public User(string v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public override string ToString()
        {
            return $"[ IdUserRole: {IdUserRole}, Login: {Login}, Password: {Password}, FirstName: {FirstName}, " +
                   $"SecondName: {SecondName}, DateBirthday: {DateBirthday}, IsBanned: {IsBanned}]";
        }
    }
}
