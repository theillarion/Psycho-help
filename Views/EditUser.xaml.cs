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
using Xk7.Model;
using Xk7.Services;

namespace Xk7.Views
{
    /// <summary>
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Page
    {
        private readonly IDbAsyncService _dbAsyncService;
        private const string TitlePage = "EditUser";
        internal EditUser(IDbAsyncService dbAsyncService, DbUser user)
        {
            InitializeComponent();
            
            UserRoleTextBox.Text = user.IdUserRole.ToString();
            LoginTextBox.Text = user.Login;
            FirstNameTextBox.Text = user.FirstName;
            SecondNameTextBox.Text = user.SecondName;
            DateBirthTextBox.Text = user.DateBirthday.ToString();



        }
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(await AdminPanel.CreateAsync());
        }
    }
}
