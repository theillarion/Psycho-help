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
using static psyho_help.logic.IAuthenfication;

namespace psyho_help.pages
{
    /// <summary>
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Page
    {
        TypeUser typeUser = TypeUser.User;
        const string DefaultName = "Введите имя...";
        const string DefaultSecondName = "Введите фамилию...";
        const string DefaultLogin = "Введите логин...";
        const string DefaultPassword = "Введите пароль...";

        internal void RemoveEmptyFields(string CurrentField)
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
        public registration()
        {
            InitializeComponent();
            RegistrationExceptionTextBox.Visibility = Visibility.Hidden;
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

        private void regRegistrationClick(object sender, RoutedEventArgs e)
        {
            // заглушка для регистрации пользователя
        }

        private void regBackClick(object sender, RoutedEventArgs e)
        {
            RegistrationFrame.Navigate(new auth());
        }
    }
}
