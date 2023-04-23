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
        Task<CommonAddResult> AddLog(string login, LoggingType loggingType);
        Task<DataTable?> GetTable(string nameTable);
        Task<UserRole> GetUserRoleAsync(string login);
        Task<bool> IsBusySlotAsync(uint idSlot);
        Task<AddSlotResult> AddSlotAsync(string employeeLogin, DateOnly slotDate, TimeOnly slotTime);
        Task<BlockSlotResult> BlockSlotAsync(uint idSlot, string userLogin);
        Task<CommonAddResult> UpdateUserTableAsync(string OldLogin, DbUser NewUser);
        Task<DataTable?> GetSlotsTableByLogin(string login);


    }
}
