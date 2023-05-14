using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xk7.Model
{
    internal interface ISlot
    {
        public uint IdTimetable { get; set; }
        public string? EmployeeLogin { get; set; }
        public DateTime SlotDate { get; set; }
        public TimeSpan SlotTime { get; set; }
    }
}
