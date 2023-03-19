using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace psyho_help.logic
{
    internal class Authenfication : IAuthenfication
    {
        public bool CheckCorrectLogin(string login)
        {
            //проверяется корректность логина по длине и пустоте textbox'a
            return login.Length > 5 && login.Contains("@");
        }

        public bool CheckCorrectPassword(string password)
        {
            return password.Any(x => char.IsLetter(x)) &&
                   password.Any(x => char.IsDigit(x)) &&
                   password.Any(x => char.IsSymbol(x)) &&
                   password.Length > 10;
        }

        public bool CheckPassCorrectUser(string password)
        {
            // заглушка
            return true;
        }


        public void CloseConnection()
        {
            throw new NotImplementedException();
        }

        public void Connection(string connectionString)
        {
            return;
        }

        public bool ExistsUser(IAuthenfication.TypeUser typeUser, string login)
        {
            //заглушка
            return true; 
        }

        public ConnectionState GetConnectionState()
        {
            throw new NotImplementedException();
        }

        public string GetHashPassword(IAuthenfication.TypeUser typeUser, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsBlockedUser(IAuthenfication.TypeUser typeUser, string login)
        {
            throw new NotImplementedException();
        }

        public bool TryAuthenfication(IAuthenfication.TypeUser typeUser, string login, string password)
        {
            throw new NotImplementedException();
        }

        public bool TryConnection(string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
