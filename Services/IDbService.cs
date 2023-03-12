using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Helper.Converts;
using App.Model;

namespace App.Services
{
    public interface IDbService
    {
        bool ExistsUser(string login);
        bool IsBannedUser(string login);
        string GetHashPassword(string login);
        AddUserResult AddUser(User user);
        DataRow? GetDataUserByLogin(string login);
    }
}
