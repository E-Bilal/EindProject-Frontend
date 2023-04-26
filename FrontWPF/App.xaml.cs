using FrontWPF.Models;
using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography.X509Certificates;
using static System.Net.WebRequestMethods;

namespace FrontWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //string url = "https://localhost:44354/api/";

        //public static RestClient client = new RestClient($"https://localhost:44354/api/Authentication");
        private void ApplicationStart(object sender, StartupEventArgs e)
        {

            //Window login = new LoginWindow();
            //login.Show();

        }
    }
}
