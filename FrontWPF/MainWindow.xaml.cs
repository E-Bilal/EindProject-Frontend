using FrontWPF.Models;
using FrontWPF.Pages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace FrontWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (User.Role == "SuperUser") 
            {
                listNav.ItemsSource = new List<NavButton>
                {
                    new NavButton("Explore", new Explore()),
                    new NavButton("Feed", new Feed()),
                    new NavButton("Search Friends", new SearchFriends()),
                    new NavButton("Friends" , new Friends()),
                    new NavButton("Messages" , new Messages()),
                    new NavButton("Settings" , new Settings()),
                    new NavButton("Admin Requests" , new AdminRequests()),
                };

            }

            else 
            {
                listNav.ItemsSource = new List<NavButton>
                {
                    new NavButton("Explore", new Explore()),
                    new NavButton("Feed", new Feed()),
                    new NavButton("Search Friends", new SearchFriends()),
                    new NavButton("Friends" , new Friends()),
                    new NavButton("Messages" , new Messages()),
                    new NavButton("Settings" , new Settings()),
                };

            }


        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }


        private void listNav_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //frame.Content = ((NavButton)listNav.SelectedItem).Page;
            var link = ((NavButton)listNav.SelectedItem).Name;

            if (link.Contains(" "))
            {
                 link = link.Replace(" ", "");
            }
            frame.Source = new System.Uri($"/Pages/{link}.xaml", UriKind.Relative);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            LoginWindow login = new LoginWindow();
            login.Show();
            Close();

            User.Id = "";
            User.UserName = "";
            User.Email = "";
            User.RoleRequest = "";
            User.Token = "";
            User.Role = "";

        }
    }
}
