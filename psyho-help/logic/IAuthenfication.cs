using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace psyho_help.logic
{
    internal interface IAuthenfication
    {
        internal enum TypeUser
        {
            User,
            Employee
        }
        void Connection(string connectionString);
        bool TryConnection(string connectionString);
        void CloseConnection();
        ConnectionState GetConnectionState();
        bool ExistsUser(TypeUser typeUser, string login);
        bool CheckPassCorrectUser(string password);
        bool IsBlockedUser(TypeUser typeUser, string login);
        string GetHashPassword(TypeUser typeUser, string password);
        bool CheckCorrectLogin(string login);
        bool CheckCorrectPassword(string password);    
        bool TryAuthenfication(TypeUser typeUser, string login, string password);
    }   
}
