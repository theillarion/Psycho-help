using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;

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
using Xk7.Helper.Exceptions;
using Xk7.pages;
using Xk7.Services;
using Xk7.Views;

namespace Xk7.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    /// 
    
    public partial class AdminPanel : Page
    {
        private readonly IDbAsyncService _dbService;
        private const string TitlePage = "AdminPanel";
        public AdminPanel()
        {
            InitializeComponent();
            var dbService = App.ConfigureDefaultDbService(App.FatalError);
            if (dbService == null)
                App.FatalError(null);
            else
                _dbService = dbService;
        }
        public static async Task<AdminPanel> CreateAsync()
        {
            var result = new AdminPanel();
            result.dbTable.DataContext = await result._dbService.GetTable("User");
            return result;
        }
    }
}
