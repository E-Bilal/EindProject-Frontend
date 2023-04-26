using FrontWPF.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.AspNetCore.SignalR.Client;
using System.Data.Common;
using System.Collections.Generic;

namespace FrontWPF
{

    public partial class LoginWindow : Window
    {
        private DispatcherTimer dispatcherTimer;
   
        private class UserModel
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
            public string RoleRequest { get; set; }
        }

        public LoginWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //Create a timer with interval of 3 secs
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);


            SignalRHub.connection = new HubConnectionBuilder().WithUrl("https://localhost:44354/TwitterHub").Build();
            SignalRHub.connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await SignalRHub.connection.StartAsync();
            };

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }


        private async Task GetUser(string UserToken)
        {

            RestSharp.client.Authenticator = new JwtAuthenticator(UserToken);
            RestRequest request = new RestRequest("Authentication");

            var response = RestSharp.client.ExecuteGet(request);
            var responseObject = JsonConvert.DeserializeObject<UserModel>(response.Content.ToString());


            User.Id = responseObject.Id;
            User.UserName = responseObject.UserName;
            User.Email = responseObject.Email;
            User.Role = responseObject.Role;
            User.RoleRequest = responseObject.RoleRequest;
            User.Token = UserToken;
            
        }

        private async void Login(object sender, RoutedEventArgs e)
        {

            var login = new Login()
            {
                Email = emailTxt.Text,
                Password = passTxt.Text,

            };



            RestRequest request = new RestRequest("Authentication/Login").AddJsonBody(login);
            var response = await RestSharp.client.ExecutePostAsync(request);

            int numericStatusCode = (int)response.StatusCode;
            
            if (numericStatusCode == 200)
            {

                var responseObject = JsonConvert.DeserializeObject<AuthResult>(response.Content.ToString());
                var token = responseObject.Token;
                
                GetUser(token);

                try
                {
                    await SignalRHub.connection.StartAsync();                  
                }
                catch 
                {
                    errorTxt.Text = "Signal Error";
                    dispatcherTimer.Start();
                    return;
                }


                MainWindow main = new MainWindow();
                main.Show();
                Close();

            }

            else if (numericStatusCode == 400)
            {
                var responseObject = JsonConvert.DeserializeObject<AuthResult>(response.Content.ToString());
                errorTxt.Text = responseObject.Error.ToString();
                dispatcherTimer.Start();

            }

            else
            {
                errorTxt.Text = "Server Error";
                dispatcherTimer.Start();
            }

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
             errorTxt.Text = "";
            //Disable the timer
            dispatcherTimer.IsEnabled = false;
        }
        private void ResetPassword(object sender, MouseButtonEventArgs e)
        {
            var passWindow = new ResetPasswordWindow();
            passWindow.Show();
            Close();
        }

        private void Register(object sender, MouseButtonEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Show();
            Close();
        }
    }

}
