using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using App.Helper;
using App.Helper.Converts;
using App.Helper.Exceptions;
using App.Model;
using App.Services;

namespace App.ViewModels
{
    internal class AuthenticationViewModel
    {
        private readonly IDbAsyncService _dbService;
        public AuthenticationViewModel(IDbAsyncService dbService)
        {
            _dbService = dbService;
        }

        public AuthenticationViewModel(DbConnection connection)
        {
            var builder = DbSettingsService.LoadDbSettings();
            connection.ConnectionString = builder.ConnectionString;
            try
            {
                _dbService = new DbAsyncService(connection);
            }
            catch (ConnectionException ex)
            {
                //Console.WriteLine(ex.Message);
            }
        }

        public async Task LoginAsync(string login, string password)
        {
            //
            Console.WriteLine("Authorization process: ");
            //
            try
            {
                var row = await _dbService.GetDataUserByLoginAsync(login);
                if (row == null)
                {
                    Console.WriteLine("The user does not exist");
                }
                else
                {
                    var user = UserFactory.FromDataRow<DbUser>(row);
                    if (Converts.ConvertByteArrayToString(user.HashPassword)
                        != Converts.ConvertStringToHeshString(password))
                    {
                        Console.WriteLine("The password is incorrect");
                    }
                    else if (user.IsBlocked)
                    {
                        Console.WriteLine("User is blocked");
                    }
                    else
                    {
                        Console.WriteLine("User has been successfully authorized");
                    }
                }
            }
            catch (ConnectionException ex)
            {
                // Не удалось открыть соединение 
                Console.WriteLine(ex.Message);
                //
            }
            catch (Exception ex) when (ex is ExecuteException or FactoryException)
            {
                // Ошибка во время выполнения запроса или фабрики 
                Console.WriteLine("An error occurred during the operation. Try again.");
                //
            }

            catch (Exception)
            {
                // Любая другая ошибка
                Console.WriteLine("An unknown error occurred. Try again");
                //
            }
        }

        public async Task RegisterAsync(User user)
        {
            //
            Console.WriteLine("Registration process: ");
            //
            try
            {
                var result = await _dbService.AddUserAsync(user);
                switch (result)
                {
                    case AddUserResult.Success:
                        //
                        Console.WriteLine("The user is successfully registered");
                        //
                        break;
                    case AddUserResult.UserExists:
                        //
                        Console.WriteLine("The user with this login already exists");
                        break;
                    default:
                        //
                        Console.WriteLine("...Error...");
                        //
                        break;
                }
            }
            catch (ConnectionException ex)
            {
                // Не удалось открыть соединение 
                Console.WriteLine(ex.Message);
                //
            }
            catch (ExecuteException ex)
            {
                // Ошибка во время выполнения запроса
                Console.WriteLine("An error occurred during the operation. Try again.");
                //
            }

            catch (Exception)
            {
                // Любая другая ошибка
                Console.WriteLine("An unknown error occurred. Try again");
                //
            }
        }
    }
}
