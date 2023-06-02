using FrontWPF.Models;
using Newtonsoft.Json;
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
    /// Interaction logic for Explore.xaml
    /// </summary>
    public partial class Explore : Page
    {
        public Explore()
        {
            InitializeComponent();
            GetTweets();
        }

        public async Task GetTweets()
        {
            RestRequest request = new RestRequest($"Tweet/GetAllTweets").AddQueryParameter("id", User.Id);
            var response = await RestSharp.client.ExecuteGetAsync(request);
            List<GetTweet> responseObject = JsonConvert.DeserializeObject<List<GetTweet>>(response.Content.ToString());

            lb_ExploreFeed.ItemsSource = responseObject;
        }

        private async void LikeTweet(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as GetTweet;
            int tweetId = rowItem.Id;
            var likeDislike = new LikeDislike()
            {
                TweetId = tweetId,
                UserId = User.Id
            };

            RestRequest request = new RestRequest("Tweet/LikeDislike").AddJsonBody(likeDislike);
            await RestSharp.client.PatchAsync(request);
            GetTweets();
        }

        private async void DeleteTweet(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure that you want to delete this tweet?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var rowItem = (sender as Button).DataContext as GetTweet;
                int tweetId = rowItem.Id;
                RestRequest request = new RestRequest($"Tweet/DeleteTweet").AddQueryParameter("tweetId", tweetId);

                try
                {
                    var response = RestSharp.client.Delete(request);
                    MessageBox.Show("Tweet Has successfuly been deleted.");
                    GetTweets();
                }


                catch
                {
                    MessageBox.Show("Something went wrong , try again later");
                }
            }
        }

    }
}
