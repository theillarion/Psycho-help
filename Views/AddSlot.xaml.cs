using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xk7.Model;
using Xk7.Services;
using Xk7.ViewModels;

namespace Xk7.Views
{
    /// <summary>
    /// Логика взаимодействия для AddSlot.xaml
    /// </summary>
    public partial class AddSlot : Page
    {
        private readonly IDbAsyncService _dbService;
        private const string TitlePage = "AddSlot";
        private DbUser _user;
        private readonly IDbAsyncService _dbAsyncService;
        private PsycholigistSlotViewModel dataViewModel;
        internal AddSlot(DbUser user)
        {
            InitializeComponent();
            
            var dbService = App.ConfigureDefaultDbService(App.FatalError);
            if (dbService == null)
                App.FatalError(null);
            else
                _user = user;

            UserLoginTextBox.Text = _user.Login;
            _dbAsyncService = dbService;
                  
            dataViewModel = new();
            UpdatePsychologistSlotsAsync();
            DataContext = dataViewModel;
        }

        private async void UpdatePsychologistSlotsAsync()
        {
            try
            {
                dataViewModel = new();
                dataViewModel.Add(await _dbAsyncService.GetFreeSlotsRows());
            }
            catch (Exception ex)
            {
                App.FatalError(ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new UserPanel(_user));

        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if  (DateSeccionTextBox.Text == String.Empty)
            {
                App.MainFrame.Navigate(new UserPanel(_user));
            }
            else
            {
                string userLogin = _user.Login;
                PsychologistSlot selectedPsychologistSlot = PsychoNameComboBox.SelectedItem as PsychologistSlot;
                string dateOnly  = selectedPsychologistSlot.SlotDate.ToString("yyyy-MM-dd");
                string timeOnly  = selectedPsychologistSlot.SlotTime.ToString("hh':'mm':'ss");
                var result = await _dbAsyncService.setBusyGetId(userLogin, dateOnly, timeOnly);

                DataRow row = result[0]; // получение первой строки из коллекции
                object value = row["Id"]; // получение значения из столбца по имени
                uint id = Convert.ToUInt32(value); // преобразование значения в тип uint
                await _dbAsyncService.InsertUserTimeTable(id, _user.Login);
                App.MainFrame.Navigate(new UserPanel(_user));
            }



        }
        private void SelectedChoosed(object sender, SelectionChangedEventArgs e)
        {
            PsychologistSlot selectedPsychologistSlot = PsychoNameComboBox.SelectedItem as PsychologistSlot;
            string dateOnly = selectedPsychologistSlot.SlotDate.ToString("dd-MM-yyyy");
            string timeOnly = selectedPsychologistSlot.SlotTime.ToString("hh':'mm':'ss");
            DateSeccionTextBox.Text = dateOnly + " " + timeOnly;

        }
    }


}
