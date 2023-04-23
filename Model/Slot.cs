using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xk7.Model
{
    public class Slot
    {
        public Slot()
        {
        }

        public Slot(uint idTimeTable, string userLogin, string employeeLogin, DateOnly slotDate, TimeOnly slotTime)
        {
            IdTimeTable = idTimeTable;
            UserLogin = userLogin;
            EmployeeLogin = employeeLogin;
            SlotDate = slotDate;
            SlotTime = slotTime;
        }

        public uint IdTimeTable { get; set; }
        public string UserLogin { get; set; }
        public string EmployeeLogin { get; set; }
        public DateOnly SlotDate { get; set; }
        public TimeOnly SlotTime { get; set; }

    }
}
