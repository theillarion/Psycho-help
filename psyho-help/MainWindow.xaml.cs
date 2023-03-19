using psyho_help.logic;
using psyho_help.pages;
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

namespace psyho_help
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    public partial class MainWindow : Window
    {
        Authenfication authenfication = new Authenfication();
        TypeUser typeUser = TypeUser.User;


        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new auth();
        }
        

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
       
        }

        private void loginTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
 
            if (loginTextBox.Text.Trim() != string.Empty){
                loginTextBox.Text = string.Empty;
                loginTextBox.SetCurrentValue(ForegroundProperty, Brushes.Black);
            }
        }

        private void loginClick(object sender, RoutedEventArgs e)
        {
            
            string login = loginTextBox.Text.Trim();
            string pass = passTextBox.Text.Trim();
            

            if (!authenfication.ExistsUser(typeUser, login) || 
                !authenfication.CheckPassCorrectUser(pass)  ||
                pass.Equals("") || login.Equals("Введите логин...") || pass.Equals("Введите пароль...")) 
            {
                loginTextBox.Text = "Некорректный данные...";
                loginTextBox.SetCurrentValue(ForegroundProperty, Brushes.Red);
                passTextBox.Text = "Некорректные данные...";
                passTextBox.SetCurrentValue(ForegroundProperty, Brushes.Red);
            }
            else{

            }
        }

        private void regButton_Click(object sender, RoutedEventArgs e)                              
        {
            Main.Content = new MainWindow();
        }

        private void passTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void passTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (passTextBox.Text.Trim() != string.Empty){
                passTextBox.Text = string.Empty;
                passTextBox.SetCurrentValue(ForegroundProperty, Brushes.Black);
            }
        }


        private void employeeLoginButton_Click(object sender, RoutedEventArgs e)
        {
            typeUser = TypeUser.Employee;
        }

    }
}
