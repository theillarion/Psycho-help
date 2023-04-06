using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xk7.Helper.Enums;
using Xk7.Model;

namespace Xk7.Services
{
    internal interface IDbAsyncService
    {
        Task<bool> ExistsUserAsync(string login);
        Task<bool> IsBannedUserAsync(string login);
        Task<string> GetHashPasswordAsync(string login);
        Task<AddUserResult> AddUserAsync(User user);
        Task<DataRow?> GetDataUserByLoginAsync(string login);
        Task<AddLoggingResult> AddLog(string login, LoggingType loggingType);
    }
}
