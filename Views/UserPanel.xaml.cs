using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        internal UserPanel(IDbAsyncService dbService, DbUser user)
        {
            InitializeComponent();
            _user = user;
            _dbService = dbService;
            this.CreateAsync();
        }

        internal async Task<UserPanel> CreateAsync()
        {
            UserPanel Result = new UserPanel(_dbService, _user);
            var Table = await Result._dbService.GetSlotsTableByLogin("test");
            var test = new ObservableCollection<Slot>();
            foreach (DataRow row in Table.Rows)
            {
                var IdTimeTable = (uint)row.ItemArray[0];
                var UserLogin = (string)row.ItemArray[1];
                var EmployeeLogin = (string)row.ItemArray[3];
                var SlotDate = (DateOnly)row.ItemArray[4];
                var SlotTime = (TimeOnly)row.ItemArray[5];
                var obj = new Slot(IdTimeTable, UserLogin, EmployeeLogin, SlotDate, SlotTime);
                test.Add(obj);
            }
            Result.dbUserSlots.ItemsSource = test;

            return Result;
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
            

        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new UserProfile(_dbService, _user));

        }
        private void SlotsButton_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

    }
}
