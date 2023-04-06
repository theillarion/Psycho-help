using Xk7.Helper.Enums;
using Xk7.Helper.Exceptions;
using Xk7.Model;
using Xk7.Services;

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
using MySql.Data.MySqlClient;

namespace Xk7.pages
{
    public partial class Registration : Page
    {
        private readonly IDbAsyncService _dbService;
        const string DefaultName = "Введите имя...";
        const string DefaultSecondName = "Введите фамилию...";
        const string DefaultLogin = "Введите логин...";
        const string DefaultPassword = "Введите пароль...";
        public Registration()
        {
            InitializeComponent();
            _dbService = ConfigureDefaultDbService();
            RegistrationExceptionTextBox.Visibility = Visibility.Hidden;
        }
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
        private void LoginTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBox regLoginTextBox = (TextBox) sender;
            if (regLoginTextBox.Text.Trim() != string.Empty)
            {
                regLoginTextBox.Text = string.Empty;
                regLoginTextBox.SetCurrentValue(ForegroundProperty, Brushes.Black);
            }
            RemoveEmptyFields("Login");
        }
        private void PassTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBox regPassTextBox = (TextBox)sender;
            if (regPassTextBox.Text.Trim() != string.Empty)
            {
                regPassTextBox.Text = string.Empty;
                regPassTextBox.SetCurrentValue(ForegroundProperty, Brushes.Black);
            }
            RemoveEmptyFields("Password");
        }
        private void NameTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBox regNameTextBox = (TextBox)sender;
            if (regNameTextBox.Text.Trim() != string.Empty)
            {
                regNameTextBox.Text = string.Empty;
                regNameTextBox.SetCurrentValue(ForegroundProperty, Brushes.Black);
            }
            RemoveEmptyFields("Name");
        }
        private void SecondNameTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBox regSecondNameTextBox = (TextBox)sender;
            if (regSecondNameTextBox.Text.Trim() != string.Empty)
            {
                regSecondNameTextBox.Text = string.Empty;
                regSecondNameTextBox.SetCurrentValue(ForegroundProperty, Brushes.Black);
            }
            RemoveEmptyFields("SecondName");
        }
        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            datePicker.SetCurrentValue(ForegroundProperty, Brushes.Black);
            RemoveEmptyFields("Date");
        }
        private void SetError(string message)
        {
            RegistrationExceptionTextBox.Text = message;
            RegistrationExceptionTextBox.Visibility = Visibility.Visible;
        }
        private async void regRegistrationClick(object sender, RoutedEventArgs e)
        {
            var login = LoginTextBox.Text.Trim();
            var password = PassTextBox.Text.Trim();
            var name = NameTextBox.Text.Trim();
            var secondName = SecondNameTextBox.Text.Trim();
            var birth = DateOnly.FromDateTime((DateTime)datePicker.SelectedDate);
            try
            {
                var user = new User(UserRole.User, login, new HashedValue(password), name, secondName, birth, false);
                var result = await _dbService.AddUserAsync(user);
                switch (result)
                {
                    case AddUserResult.Success:
                        MessageBox.Show("User successfully registered", "Authentication", MessageBoxButton.OK, MessageBoxImage.Information);
                        await _dbService.AddLog(login, LoggingType.SuccessRegistration);
                        break;
                    case AddUserResult.UserExists:
                        SetError("The user with this login already exists.");
                        break;
                    default:
                        SetError("Common error.");
                        break;
                }
            }
            catch (ConnectionException ex)
            {
                SetError(ex.Message);
            }
            catch (ExecuteException ex)
            {
                SetError("An error occurred during the operation. Try again.");
            }
            catch (Exception)
            {
                SetError("An unknown error occurred. Try again");
            }
        }
        private void regBackClick(object sender, RoutedEventArgs e)
        {
            RegistrationFrame.Navigate(new Auth());
        }
        // TODO: Fix
        private void RemoveEmptyFields(string CurrentField)
        {
            switch (CurrentField)
            {
                case "Name":
                    if (SecondNameTextBox.Text.Trim() == string.Empty)
                    {
                        SecondNameTextBox.Text = DefaultSecondName;
                        SecondNameTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                    }
                    if (PassTextBox.Text.Trim() == string.Empty)
                    {
                        PassTextBox.Text = DefaultPassword;
                        PassTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                    }
                    if (LoginTextBox.Text.Trim() == string.Empty)
                    {
                        LoginTextBox.Text = DefaultLogin;
                        LoginTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                    }
                    break;

                case "SecondName":
                    if (NameTextBox.Text.Trim() == string.Empty)
                    {
                        NameTextBox.Text = DefaultName;
                        NameTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                    }
                    if (PassTextBox.Text.Trim() == string.Empty)
                    {
                        PassTextBox.Text = DefaultPassword;
                        PassTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                    }
                    if (LoginTextBox.Text.Trim() == string.Empty)
                    {
                        LoginTextBox.Text = DefaultLogin;
                        LoginTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                    }
                    break;
                case "Login":
                    if (NameTextBox.Text.Trim() == string.Empty)
                    {
                        NameTextBox.Text = DefaultName;
                        NameTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                    }

                    if (SecondNameTextBox.Text.Trim() == string.Empty)
                    {
                        SecondNameTextBox.Text = DefaultSecondName;
                        SecondNameTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                    }
                    if (PassTextBox.Text.Trim() == string.Empty)
                    {
                        PassTextBox.Text = DefaultPassword;
                        PassTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                    }
                    break;
                case "Password":
                    if (NameTextBox.Text.Trim() == string.Empty)
                    {
                        NameTextBox.Text = DefaultName;
                        NameTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                    }
                    if (SecondNameTextBox.Text.Trim() == string.Empty)
                    {
                        SecondNameTextBox.Text = DefaultSecondName;
                        SecondNameTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                    }
                    if (LoginTextBox.Text.Trim() == string.Empty)
                    {
                        LoginTextBox.Text = DefaultLogin;
                        LoginTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                    }
                    break;
                case "Date":
                    if (NameTextBox.Text.Trim() == string.Empty)
                    {
                        NameTextBox.Text = DefaultName;
                        NameTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                    }
                    if (SecondNameTextBox.Text.Trim() == string.Empty)
                    {
                        SecondNameTextBox.Text = DefaultSecondName;
                        SecondNameTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                    }
                    if (LoginTextBox.Text.Trim() == string.Empty)
                    {
                        LoginTextBox.Text = DefaultLogin;
                        LoginTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                    }
                    if (PassTextBox.Text.Trim() == string.Empty)
                    {
                        PassTextBox.Text = DefaultPassword;
                        PassTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
                    }
                    break;
            }
        }
    }
}
