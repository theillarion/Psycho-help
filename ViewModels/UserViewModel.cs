using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xk7.Helper;
using Xk7.Model;

namespace Xk7.ViewModels
{
    class UserViewModel
    {
        public ObservableCollection<DbUser> Users { get; set; }
        public UserViewModel()
        {
            Users = new();
        }
        public static ObservableCollection<DbUser>? ConvertFromDataRowCollection(DataRowCollection? dataRows)
        {
            if (dataRows == null)
                return null;
            var users = new ObservableCollection<DbUser>();

            foreach (DataRow row in dataRows)
            {
                var user = Factory.FromDataRow<DbUser>(row);
                if (user != null)
                {
                    users.Add(user);
                }
            }
            return users;

        }
        public UserViewModel(DataRowCollection? dataRow)
        {
            Users = ConvertFromDataRowCollection(dataRow) ?? new();
        }
        public void Add(DbUser user)
        {
            if (user != null)
                Users.Add(user);
        }
        public void Add(DataRowCollection? dataRow)
        {
            var newUsers = ConvertFromDataRowCollection(dataRow);
            if (newUsers != null)
                foreach (var user in newUsers)
                    Users.Add(user);
        }
    }
}
