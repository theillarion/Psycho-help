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
    class NotificationViewModel
    {
        public ObservableCollection<Notification> Notifies { get; set; }
        public NotificationViewModel()
        {
            Notifies = new();
        }
        public static ObservableCollection<Notification>? ConvertFromDataRowCollection(DataRowCollection? dataRows)
        {
            if (dataRows == null)
                return null;
            var notifies = new ObservableCollection<Notification>();

            foreach (DataRow row in dataRows)
            {
                var notify = Factory.FromDataRow<Notification>(row);
                if (notify != null)
                {
                    notifies.Add(notify);
                }
            }
            return notifies;

        }
        public NotificationViewModel(DataRowCollection? dataRow)
        {
            Notifies = ConvertFromDataRowCollection(dataRow) ?? new();
        }
        public void Add(Notification notification)
        {
            if (notification != null)
                Notifies.Add(notification);
        }
        public void Add(DataRowCollection? dataRow)
        {
            var newNotifications = ConvertFromDataRowCollection(dataRow);
            if (newNotifications != null)
                foreach (var notification in newNotifications)
                    Notifies.Add(notification);
        }
    }
}
