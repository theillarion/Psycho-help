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

namespace psyho_help.pages
{
    /// <summary>
    /// Логика взаимодействия для auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        // // Authenfication authenfication = new Authenfication();
        // По дефолту при входе в окно авторизации клиентом является User
        // // TypeUser typeUser = TypeUser.User;
        const string DefaultLogin = "Введите логин...";
        const string DefaultPassword = "Введите пароль...";

        public Auth()
        {
            InitializeComponent();
            AuthExceptionTextBox.Visibility = Visibility.Hidden;
        }
        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}
        //private void passTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //}

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

        private void loginClick(object sender, RoutedEventArgs e)
        {
            AuthExceptionTextBox.Visibility = Visibility.Hidden;
            string Login = loginTextBox.Text.Trim();
            string Pass = passTextBox.Text.Trim();
            // заглушка базы данных провереки принадлежности user'a БД
            bool ExistsUser = false;
            bool CorrectPassword = true;
            if (ExistsUser)
            {
                if (CorrectPassword) 
                {
                    //authFrame.Navigate(new PersonalAccount());

                }
                else
                {
                    AuthExceptionTextBox.Text = "Некорректный пароль, попробуйте снова.";
                    AuthExceptionTextBox.Visibility = Visibility.Visible;
                }
            }
            else
            {
                AuthExceptionTextBox.Text = "Пользователь не существует или логин введен неверно.";
                AuthExceptionTextBox.Visibility = Visibility.Visible;
            }
            
        }
        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            // Клиент выбирает кнопку зарегестрироваться в поле авторизации
            authFrame.Navigate(new Registration());
        }
        private void employeeLoginButton_Click(object sender, RoutedEventArgs e)
        {
            AuthExceptionTextBox.Visibility = Visibility.Hidden;
            //typeUser = TypeUser.Employee;
            loginTextBox.Text = DefaultLogin;
            loginTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
            passTextBox.Text = DefaultPassword;
            passTextBox.SetCurrentValue(ForegroundProperty, Brushes.Gray);
        }

        private void authFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
