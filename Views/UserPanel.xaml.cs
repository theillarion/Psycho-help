using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xk7.Model;
using Xk7.pages;
using Xk7.Services;
using Xk7.ViewModels;

namespace Xk7.Views
{
    /// <summary>
    /// Логика взаимодействия для UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Page
    {
        private readonly IDbAsyncService _dbService;
        private const string TitlePage = "UserPanel";
        private DbUser _user;
        private readonly IDbAsyncService _dbAsyncService;
        private SlotViewModel dataViewModel;
        internal UserPanel(DbUser user)
        {
            InitializeComponent();
            var dbService = App.ConfigureDefaultDbService(App.FatalError);
            if (dbService == null)
                App.FatalError(null);
            else
                _user = user;
                _dbAsyncService = dbService;

            dataViewModel = new();
            UpdateSlotsAsync();
            DataContext = dataViewModel;
          
        }

        private async void UpdateSlotsAsync()
        {
            try
            {
                dataViewModel = new();
                dataViewModel.Add(await _dbAsyncService.GetSlotsRowsByLogin(_user.Login));
            }
            catch (Exception ex)
            {
                App.FatalError(ex.Message);
            }
        }

        internal void CreateAsync()
        {
            /*var Result = new UserPanel(_user);
            var Table = await Result._dbService.GetTable("Timetable");
            var test = new ObservableCollection<Slot>();
            foreach (DataRow row in Table.Rows)
            {
                Slot obj = new Slot((int)row.ItemArray[0], (string)row.ItemArray[1], (DateTime)(row.ItemArray[2]), (TimeSpan)row.ItemArray[3]);
                test.Add(obj);
            }
            Result.dbUserSlots.ItemsSource = test;

            return Result;*/
            dataViewModel = new();
            UpdateSlotsAsync();
            DataContext = dataViewModel;

        }

        private void ChangeLanguageClick(object sender, RoutedEventArgs e)
        {
            if (App.language.Equals("ru"))
            {
                App.language = "en";
                UICultureService.SetCulture(new CultureInfo(App.language));
            }
            else
            {
                App.language = "ru";
                UICultureService.SetCulture(new CultureInfo(App.language));
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new Auth(_dbService));
        }

        private void AddSlotButton_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new AddSlot(_user));

        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new UserProfile(_dbService, _user));

        }
        private async void SlotsButton_Click(object sender, RoutedEventArgs e)
        {
            CreateAsync();

        }

        private async void UserPanelDeleteButton(object sender, RoutedEventArgs e)
        {
            var slot = dbUserSlots.SelectedItem as Slot;
            await _dbAsyncService.DeleteSlot(slot.IdTimetable);
            dataViewModel = new();
            UpdateSlotsAsync();
            DataContext = dataViewModel;
        }

    }
}
