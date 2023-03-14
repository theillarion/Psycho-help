using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Helper.Enums;
using App.Model;

namespace App.Services
{
    internal interface IDbAsyncService
    {
        Task<bool> ExistsUserAsync(string login);
        Task<bool> IsBannedUserAsync(string login);
        Task<string> GetHashPasswordAsync(string login);
        Task<AddUserResult> AddUserAsync(User user);
        Task<DataRow?> GetDataUserByLoginAsync(string login);
    }
}
