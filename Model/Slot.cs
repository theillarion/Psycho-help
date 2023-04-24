﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xk7.Helper.Enums;

namespace Xk7.Model
{
    public class Slot : ISlot
    {
        public uint Id { get; set; }
        public string? EmployeeLogin { get; set; }
        public DateOnly SlotDate { get; set; }
        public TimeOnly SlotTime { get; set; }
        public Slot() {}

        public Slot(uint idTimeTable, string employeeLogin, DateOnly slotDate, TimeOnly slotTime)
        {
            Id = idTimeTable;
            EmployeeLogin = employeeLogin;
            SlotDate = slotDate;
            SlotTime = slotTime;
        }

    }
}
