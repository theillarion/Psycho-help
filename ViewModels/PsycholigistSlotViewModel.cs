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
    class PsycholigistSlotViewModel
    {
        public ObservableCollection<PsychologistSlot> PsycholigistSlots { get; set; }
        public PsycholigistSlotViewModel()
        {
            PsycholigistSlots = new();
        }
        public static ObservableCollection<PsychologistSlot>? ConvertFromDataRowCollection(DataRowCollection? dataRows)
        {
            if (dataRows == null)
                return null;
            var psychoSlots = new ObservableCollection<PsychologistSlot>();

            foreach (DataRow row in dataRows)
            {
                var psychoSlot = Factory.FromDataRow<PsychologistSlot>(row);
                if (psychoSlot != null)
                {
                    psychoSlots.Add(psychoSlot);
                }
            }
            return psychoSlots;

        }
        public PsycholigistSlotViewModel(DataRowCollection? dataRow)
        {
            PsycholigistSlots = ConvertFromDataRowCollection(dataRow) ?? new();
        }
        public void Add(PsychologistSlot psychoSlot)
        {
            if (psychoSlot != null)
                PsycholigistSlots.Add(psychoSlot);
        }
        public void Add(DataRowCollection? dataRow)
        {
            var newPsychoSlots = ConvertFromDataRowCollection(dataRow);
            if (newPsychoSlots != null)
                foreach (var psychoSlot in newPsychoSlots)
                    PsycholigistSlots.Add(psychoSlot);
        }
    }
}
