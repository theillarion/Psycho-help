using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xk7.Helper.Enums;

namespace Xk7.Model
{
    public class Slot : ISlot, INotifyPropertyChanged
    {
        private uint id;
        private string employeeLogin;
        private DateTime slotDate;
        private TimeSpan slotTime;
        public uint IdTimetable
        {
            get 
            {
                return id; 
            }
            set
            {
                id = value;
                OnPropertyChanged("IdTimetable");
            }
        }

        public string? EmployeeLogin
        {
            get
            {
                return employeeLogin;
            }
            set
            {
                employeeLogin = value;
                OnPropertyChanged("EmployeeLogin");
            }
        }
        public DateTime SlotDate
        {
            get
            {
                return slotDate;
            }
            set
            {
                slotDate = value;
                OnPropertyChanged("SlotDate");
            }
        }
        public TimeSpan SlotTime
        {
            get
            {
                return slotTime;
            }
            set
            {
                slotTime = value;
                OnPropertyChanged("SlotTime");
            }
        }
        public Slot() {}

        public Slot(uint idTimeTable, string employeeLogin, DateTime slotDate, TimeSpan slotTime)
        {
            IdTimetable = idTimeTable;
            EmployeeLogin = employeeLogin;
            SlotDate = slotDate;
            SlotTime = slotTime;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
