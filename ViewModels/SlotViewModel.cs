using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xk7.Model;
using Xk7.Helper;
using Xk7.Helper.Extensions;

namespace Xk7.ViewModels
{
    class SlotViewModel
    {
        public ObservableCollection<Slot> Slots { get; set; }
        public SlotViewModel()
        {
            Slots = new();
        }
        public static ObservableCollection<Slot>? ConvertFromDataRowCollection(DataRowCollection? dataRows)
        {
            if (dataRows == null)
                return null;
            var slots = new ObservableCollection<Slot>();

            foreach (DataRow row in dataRows)
            {
                var slot = Factory.FromDataRow<Slot>(row);
                if (slot != null)
                {
                    slots.Add(slot);
                }
            }
            return slots;

        }
        public SlotViewModel(DataRowCollection? dataRow)
        {
            Slots = ConvertFromDataRowCollection(dataRow) ?? new();
        }
        public void Add(Slot slot)
        {
            if (slot != null)
                Slots.Add(slot);
        }
        public void Add(DataRowCollection? dataRow)
        {
            var newSlots = ConvertFromDataRowCollection(dataRow);
            if (newSlots != null)
                foreach (var slot in newSlots)
                    Slots.Add(slot);
        }
    }
}
