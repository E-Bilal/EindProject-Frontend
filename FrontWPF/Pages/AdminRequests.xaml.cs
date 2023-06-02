using FrontWPF.Models;
using Newtonsoft.Json;
using RestSharp.Authenticators;
using RestSharp;
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

namespace FrontWPF.Pages
{
    /// <summary>
    /// Interaction logic for AdminRequests.xaml
    /// </summary>
    public partial class AdminRequests : Page
    {
        public AdminRequests()
        {
            InitializeComponent();
            GetAdminRequets();
        }



        public async Task GetAdminRequets()
        {
            RestSharp.client.Authenticator = new JwtAuthenticator(User.Token);
            RestRequest request = new RestRequest($"AdminRequests/GetAdminRequests");
            var response = await RestSharp.client.ExecuteGetAsync(request);
            List<GetAdminRequests> responseObject = JsonConvert.DeserializeObject<List<GetAdminRequests>>(response.Content.ToString());
            lb_AdminRequests.ItemsSource = responseObject;
        }

        private async void AcceptRequest(object sender, RoutedEventArgs e)
        {

            var rowItem = (sender as Button).DataContext as GetAdminRequests;
            string userId = rowItem.Id;
            RestSharp.client.Authenticator = new JwtAuthenticator(User.Token);
            RestRequest request = new RestRequest("AdminRequests/AcceptAdminRole").AddQueryParameter("id", userId);
            

            try
            {
                await RestSharp.client.PatchAsync(request);
                GetAdminRequets();
            }

            catch
            {
                MessageBox.Show("Something went wrong , try again later.");
            }
        }

        private async void RejectRequest(object sender, RoutedEventArgs e)
        {

            var rowItem = (sender as Button).DataContext as GetAdminRequests;
            string userId = rowItem.Id;
            RestSharp.client.Authenticator = new JwtAuthenticator(User.Token);
            RestRequest request = new RestRequest("AdminRequests/RejectAdminRole").AddQueryParameter("id", userId);

            try
            {
                await RestSharp.client.PatchAsync(request);
                GetAdminRequets();
            }

            catch
            {
                MessageBox.Show("Something went wrong , try again later.");
            }
        }

        private async void RevokeRequest(object sender, RoutedEventArgs e)
        {

            var rowItem = (sender as Button).DataContext as GetAdminRequests;
            string userId = rowItem.Id;
            MessageBox.Show(userId);
            RestSharp.client.Authenticator = new JwtAuthenticator(User.Token);
            RestRequest request = new RestRequest("Settings/RevokeAdminRole").AddQueryParameter("id", userId);

            try
            {
                await RestSharp.client.PatchAsync(request);
                GetAdminRequets();
            }

            catch
            {
                MessageBox.Show("Something went wrong , try again later.");
            }
        }

    }
}
