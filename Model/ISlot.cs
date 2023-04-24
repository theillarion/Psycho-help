using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xk7.Model
{
    internal interface ISlot
    {
        public uint Id { get; set; }
        public string? EmployeeLogin { get; set; }
        public DateOnly SlotDate { get; set; }
        public TimeOnly SlotTime { get; set; }
    }
}
