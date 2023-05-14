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
    class PsychologistSlot : INotifyPropertyChanged
    {
        public string? login { get; set; }
        private DateTime slotDate;
        private TimeSpan slotTime;

        public override string ToString()
        {
            return Login;
        }

        public string? Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
                OnPropertyChanged("IdTimetable");
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
                OnPropertyChanged("IdTimetable");
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
                OnPropertyChanged("IdTimetable");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
