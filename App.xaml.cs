using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Xk7.Services;

namespace Xk7
{
    public partial class App : Application
    {
        App()
        {
            UICultureService.SetCulture(new CultureInfo("en"));
        }
    }
}
