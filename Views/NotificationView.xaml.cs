using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using Xk7.Helper.Exceptions;
using Xk7.Model;
using Xk7.pages;
using Xk7.Services;
using Xk7.ViewModels;

namespace Xk7.Views
{
    /// <summary>
    /// Логика взаимодействия для NotificationView.xaml
    /// </summary>
    public partial class NotificationView : Page
    {
        private readonly IDbAsyncService _dbAsyncService;
        private static DbUser _user;
        private const string TitlePage = "Notification";
        private NotificationViewModel dataViewModel;
        private DispatcherTimer timer;

        internal NotificationView(DbUser user)
        {
            _user = user;
            InitializeComponent();
            var dbService = App.ConfigureDefaultDbService(App.FatalError);
            if (dbService == null)
                App.FatalError(null);
            else
                _dbAsyncService = dbService;

            dataViewModel = new();
            DataContext = dataViewModel;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Tick += UpdateNotificationsAsync;
            timer.Start();
        }
        public static async Task<NotificationView> CreateAsync()
        {
            var result = new NotificationView(_user);
            try
            {
                result.dataViewModel.Add(await result._dbAsyncService.GetAllNotifications(_user.Login, DateTime.FromFileTimeUtc(0)));
            }
            catch (ConnectionException)
            {
                App.FatalError("Refused connection");
            }
            catch (Exception ex)
            {
                App.FatalError(ex.Message);
            }
            return result;
        }
        private async void UpdateNotificationsAsync(object sender, EventArgs e)
        {
            try
            {
                dataViewModel.Add(await _dbAsyncService.GetNewNotifications(_user.Login));
            }
            catch (Exception ex)
            {
                timer.Stop();
                App.FatalError(ex.Message);
            }
        }


        private void ExitClick(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new Auth(_dbAsyncService));
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new UserProfile(_dbAsyncService, _user));
        }

        private void SlotsButton_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new UserPanel(_user));
        }
    }
}
