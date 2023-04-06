using Xk7.Helper.Converts;
using Xk7.Helper.Exceptions;
using Xk7.Helper;
using Xk7.Model;

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
using Xk7.Services;
using MySql.Data.MySqlClient;
using Xk7.Helper.Enums;

namespace Xk7.pages
{
    public partial class Auth : Page
    {
        private readonly IDbAsyncService _dbService;
        const string DefaultLogin = "Введите логин...";
        const string DefaultPassword = "Введите пароль...";
        private DbAsyncService ConfigureDefaultDbService()
        {
            if (!DbSettingsService.DbSettingsFileExists())
            {
                MessageBox.Show("Not found file configuration", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
            var settings = DbSettingsService.LoadDbSettings();
            try
            {
                return new DbAsyncService(new MySqlConnection(settings.ConnectionString));
            }
            catch (ConnectionException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
            return null;
        }
        public Auth()
        {
            InitializeComponent();
            _dbService = ConfigureDefaultDbService();
            AuthExceptionTextBox.Visibility = Visibility.Hidden;
        }
        private void SetError(string message)
        {
            AuthExceptionTextBox.Text = message;
            AuthExceptionTextBox.Visibility = Visibility.Visible;
        }
        private void loginTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (loginTextBox.Text.Equals(DefaultLogin))
            {
                loginTextBox.Text = string.Empty;
                loginTextBox.SetCurrentValue(ForegroundProperty, Brushes.Black);
            }
            // проверка, если при переходе в поле ввода логина, поле пароля так и не было заполнено
            if (passTextBox.Text.Trim() == string.Empty)
            {
                passTextBox.Text = DefaultPassword;
                passTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
            }
        }
        private void passTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (passTextBox.Text.Equals(DefaultPassword))
            {
                passTextBox.Text = string.Empty;
                passTextBox.SetCurrentValue(ForegroundProperty, Brushes.Black);
            }
            // проверка, если при переходе в поле ввода пароля, поле логина так и не было заполнено
            if (loginTextBox.Text.Trim() == string.Empty)
            {
                loginTextBox.Text = DefaultLogin;
                loginTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
            }
        }
        private async void loginClick(object sender, RoutedEventArgs e)
        {
            var login = loginTextBox.Text.Trim();
            var password = passTextBox.Text.Trim();
            try
            {
                var row = await _dbService.GetDataUserByLoginAsync(login);
                if (row == null)
                    SetError("The user does not exist.");
                else
                {
                    var user = UserFactory.FromDataRow<DbUser>(row);
                    if (user == null)
                        SetError("An unknown error occurred. Try again.");
                    else if (user.IsBlocked)
                    {
                        SetError("User is blocked.");
                        await _dbService.AddLog(login, LoggingType.FailedAuthenticationUserBanned);
                    }
                        
                    else if (Converts.ConvertByteArrayToString(user.HashPassword)
                             != Converts.ConvertStringToHeshString(password))
                    {
                        SetError("The password is incorrect.");
                        await _dbService.AddLog(login, LoggingType.FailedAuthenticationWrongPassword);
                    }
                    else
                    {
                        MessageBox.Show("User has been successfully authorized", "Authentication", MessageBoxButton.OK, MessageBoxImage.Information);
                        await _dbService.AddLog(login, LoggingType.SuccessAuthentication);
                    }
                }
            }
            catch (ConnectionException)
            {
                SetError("Connection refused.");
            }
            catch (Exception ex) when (ex is ExecuteException or FactoryException)
            {
                SetError("An error occurred during the operation. Try again.");
            }
            catch (Exception)
            {
                SetError("An unknown error occurred. Try again.");
            }    
        }
        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            authFrame.Navigate(new Registration());
        }
        private async void employeeLoginButton_Click(object sender, RoutedEventArgs e)
        {
            loginClick(sender, e);
        }
    }
}
