using MySql.Data.MySqlClient;

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
using System.Windows.Shapes;
using System.Windows.Threading;

using Xk7.Helper.Exceptions;

using Xk7.Services;

using Xk7.ViewModels;

namespace Xk7.Views
{
    /// <summary>
    /// Логика взаимодействия для NotificationView.xaml
    /// </summary>
    public partial class NotificationView : Window
    {
        private readonly IDbAsyncService _dbAsyncService;
        private const string TitlePage = "Notification";
        private NotificationViewModel dataViewModel;
        private DispatcherTimer timer;
        internal NotificationView(IDbAsyncService dbAsyncService)
        {
            InitializeComponent();
            _dbAsyncService = dbAsyncService;
        }
        public static async Task<NotificationView> CreateAsync()
        {
            var result = new NotificationView();
            try
            {
                result.dataViewModel.Add(await result._dbAsyncService.GetAllNotifications("Admin", DateTime.FromFileTimeUtc(0)));
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
        public NotificationView()
        {
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
        private async void UpdateNotificationsAsync(object sender, EventArgs e)
        {
            try
            {
                dataViewModel.Add(await _dbAsyncService.GetNewNotifications("Admin"));
            }
            catch (Exception ex)
            {
                timer.Stop();
                App.FatalError(ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime time = DateTime.FromFileTimeUtc(0);
        }
    }
}
