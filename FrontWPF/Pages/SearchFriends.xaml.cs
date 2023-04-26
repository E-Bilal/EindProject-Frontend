using FrontWPF.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SearchFriends.xaml
    /// </summary>
    public partial class SearchFriends : Page
    {
        //SearchFriend searchFriend = new SearchFriend();

        public SearchFriends()
        {
            InitializeComponent();
            GetSeacrchFriends();



        }


        public async Task GetSeacrchFriends()
        {

            RestClient client = new RestClient($"https://localhost:44354/api/Friends/SearchFriends?id={User.Id}");
            client.Authenticator = new JwtAuthenticator(User.Token);
            var response = await client.ExecuteGetAsync(new RestRequest());
            //var responseObject = JsonConvert.DeserializeObject<SearchFriend>(response.Content.ToString());
            List<SearchFriend> responseObject = JsonConvert.DeserializeObject<List<SearchFriend>>(response.Content.ToString());

            lb_SearchFriends.ItemsSource = responseObject;

        }

        public async void MouseEnter(object sender, RoutedEventArgs e)
        {

            
            var rowItem = (sender as Button).DataContext as SearchFriend;
            string friendId = rowItem.Id;
            RestClient client = new RestClient($"https://localhost:44354/api/Friends/SendFriendRequest?id={User.Id}&FriendId={friendId}");
            client.Authenticator = new JwtAuthenticator(User.Token);
            await client.PatchAsync(new RestRequest());
            GetSeacrchFriends();
            //try
            //{
            //    int index = 0;
            //    index = lb_SearchFriends.SelectedIndex;
            //    MessageBox.Show(index.ToString());

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
        }
 
    }
}
