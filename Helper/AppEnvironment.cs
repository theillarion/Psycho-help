using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace App.Helper
{
    public static class AppEnvironment
    {
        public static string GetRootWorkDirectory()
        {
            var projectPath = Directory.GetCurrentDirectory();
            while (projectPath != null && !Directory.Exists(Path.Combine(projectPath, "bin")))
            {
                projectPath = Directory.GetParent(projectPath)?.FullName;
            }
            return projectPath ?? "";
        }
    }
}
