﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
using Xk7.pages;
using Xk7.Services;

namespace Xk7.Views
{
    /// <summary>
    /// Логика взаимодействия для UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Page
    {
        private readonly IDbAsyncService _dbAsyncService;
        private DbUser _user;
        private const string TitlePage = "EditUser";
        internal UserProfile(IDbAsyncService dbAsyncService, DbUser user)
        {
            InitializeComponent();
            _dbAsyncService = dbAsyncService;
            _user = user;
            LoginTextBlock.Text = user.Login;
            FirstNameTextBlock.Text = user.FirstName;
            SecondNameTextBlock.Text = user.SecondName;
            DateBirthTextBlock.Text = user.DateBirthday.ToString();
        }

        private void ChangeLanguageClick(object sender, RoutedEventArgs e)
        {
            if (App.language.Equals("ru"))
            {
                App.language = "en";
                UICultureService.SetCulture(new CultureInfo(App.language));
            }
            else
            {
                App.language = "ru";
                UICultureService.SetCulture(new CultureInfo(App.language));
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new Auth(_dbAsyncService));
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void SlotsButton_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new UserPanel(_dbAsyncService, _user));
        }
    }
}
