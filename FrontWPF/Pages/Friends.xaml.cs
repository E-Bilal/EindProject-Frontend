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
    /// Interaction logic for Friends.xaml
    /// </summary>
    public partial class Friends : Page
    {
        public Friends()
        {
            InitializeComponent();
            GetFriends();
        }


        public async Task GetFriends()
        {

            RestClient client = new RestClient($"https://localhost:44354/api/Friends/GetFriends?id={User.Id}");
            client.Authenticator = new JwtAuthenticator(User.Token);
            var response = await client.ExecuteGetAsync(new RestRequest());
            //var responseObject = JsonConvert.DeserializeObject<SearchFriend>(response.Content.ToString());
            List<SearchFriend> responseObject = JsonConvert.DeserializeObject<List<SearchFriend>>(response.Content.ToString());

            lb_SearchFriends.ItemsSource = responseObject;

        }

        private async void AcceptFriendRequest(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as SearchFriend;
            string friendId = rowItem.Id;
            RestClient client = new RestClient($"https://localhost:44354/api/Friends/AcceptFriendRequest?id={User.Id}&FriendId={friendId}");
            client.Authenticator = new JwtAuthenticator(User.Token);
            await client.PatchAsync(new RestRequest());
            GetFriends();

        }

        private async void RejectFriendRequest(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as SearchFriend;
            string friendId = rowItem.Id;
            RestClient client = new RestClient($"https://localhost:44354/api/Friends/RejectFriendRequest?id={User.Id}&FriendId={friendId}");
            client.Authenticator = new JwtAuthenticator(User.Token);
            await client.PatchAsync(new RestRequest());
            GetFriends();
        }

        private async void Unfriend(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as SearchFriend;
            string friendId = rowItem.Id;
            RestClient client = new RestClient($"https://localhost:44354/api/Friends/RejectFriendRequest?id={User.Id}&FriendId={friendId}");
            client.Authenticator = new JwtAuthenticator(User.Token);
            await client.PatchAsync(new RestRequest());
            GetFriends();
        }
    }
}
