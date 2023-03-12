using System.Data;
using App.Helper;
using App.Helper.Converts;
using App.Helper.Enums;
using App.Model;
using App.Services;
using App.ViewModels;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;

namespace App
{
    internal class Program
	{
        private static async Task Main()
        {
            var settings = DbSettingsService.LoadDbSettings();
            var connection = new MySqlConnection(settings.ConnectionString);
            var auth = new AuthenticationViewModel(connection);

            var user = new User(UserRole.User, "TestUser", new HashedValue("TestPass"), "TestName", "TestSurname",
                new DateOnly(2000, 1, 1), false);
            
            await auth.RegisterAsync(user);
            await auth.LoginAsync(user.Login, user.Password.OriginalValue);
        }
	}
}
