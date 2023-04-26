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
using System.Globalization;
using System.Data.Common;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Win32;

namespace FrontWPF.Pages
{
    /// <summary>
    /// Interaction logic for Messages.xaml
    /// </summary>
    public partial class Messages : Page
    {
        static string friendId = "";
        string friendUsername = "";
        
        public Messages()
        {
            InitializeComponent();
            GetFriends();
            SignalRHub.connection.On<string>("Posted",(value) =>
                {
                    Dispatcher.InvokeAsync((Action)(async () =>
                    {                           
                       GetMessages();
                    }));
                });
        }

        public async Task GetFriends()
        {        
            RestSharp.client.Authenticator = new JwtAuthenticator(User.Token);
            RestRequest request = new RestRequest($"Friends/GetAcceptedFriends?id={User.Id}");
            var response = await RestSharp.client.ExecuteGetAsync(request);
            List<SearchFriend> responseObject = JsonConvert.DeserializeObject<List<SearchFriend>>(response.Content.ToString());

            lv_friends.ItemsSource = responseObject;
        }

        private async void SendMessage(object sender, RoutedEventArgs e)
        {            
            var message = new SendChat()
            {
                UserId = User.Id,
                FriendId =friendId,
                Message = Message.Text
            };

            RestRequest request = new RestRequest("Chat/SendMessage").AddJsonBody(message);
            RestSharp.client.Authenticator = new JwtAuthenticator(User.Token);
            var response = await RestSharp.client.ExecutePostAsync(request);
        }

        private  async void GetMessages()
        {

            RestSharp.client.Authenticator = new JwtAuthenticator(User.Token);
            RestRequest request = new RestRequest($"Chat/GetMessages?userId={User.Id}&friendId={friendId}");

            var response = await RestSharp.client.ExecuteGetAsync(request);
            List<Chat> responseObject = JsonConvert.DeserializeObject<List<Chat>>(response.Content.ToString());

                        
            foreach (var item in responseObject)
            {
                item.Timeformatted = item.currentTime?.ToString("HH:mm");
                if (item.ApplicationUserId== User.Id)
                {                   
                    item.Username = User.UserName;
                }
                else item.Username = friendUsername;
                
            }
            
            for (int i =1; i < responseObject.Count; i++)
            {
                if (responseObject[i].ApplicationUserId == responseObject[i -1].ApplicationUserId)
                {                   
                    responseObject[i].UsernameVisible = "Collapsed";
                }

                else 
                {
                    responseObject[i].UsernameVisible = "Visible";                    
                }
            }
            lv_chat.ItemsSource = responseObject;          
        }

        private void lv_friends_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_friends.SelectedItem != null)
            {
                friendId = ((SearchFriend)lv_friends.SelectedItem).Id;
                friendUsername = ((SearchFriend)lv_friends.SelectedItem).Username;

                GetMessages();
                Messagebox.Visibility = Visibility.Visible;                
            }
        }
    }
}
