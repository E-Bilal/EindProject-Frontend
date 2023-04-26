using FrontWPF.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using System.Windows.Threading;

namespace FrontWPF
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private DispatcherTimer dispatcherTimer;
        public RegisterWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;


        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void BackToLogin(object sender, MouseButtonEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private async void Register(object sender, RoutedEventArgs e)
        {
            var email = new EmailAddressAttribute();
            responseTxt.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#D7596D");


            //Email Validation

            if (!email.IsValid(emailTxt.Text))
            {
                //MessageBox.Show("Please enter a valid email address");
                responseTxt.Text = "Please enter a valid email address";
                emailTxt.Text = "";
                return;
            }

            // Username Validation
            string errorUsername = "";

            if (usernameTxt.Text.Length < 6 || usernameTxt.Text.Length > 25)
            {
                errorUsername = errorUsername + "\n- At least 6 Characters long and less than 25 characters";
            }

            if (!usernameTxt.Text.All(char.IsLetterOrDigit))
            {
                errorUsername = errorUsername + "\n- Username can only contain alphanumeric characters.";
            }

            if (errorUsername != "")
            {                
                responseTxt.Text = $"Username should meet these requirements: {errorUsername}";
                usernameTxt.Text = "";
                return;
            }

            // Password Validation
            string errorPassword = "";

            if (passwordTxt.Text != confirmPasswordTxt.Text)
            {
                responseTxt.Text = "Passwords are not the same try again";
                return;
            }

            if (passwordTxt.Text.Length < 6)
            {
                errorPassword = errorPassword + "\n- 6 Characters long ";
            }
            if (!passwordTxt.Text.Any(char.IsUpper))
            {
                errorPassword = errorPassword + "\n- At least one uppercase ('A'-'Z').";
            }
            if (!passwordTxt.Text.Any(char.IsDigit))
            {
                errorPassword = errorPassword + "\n- At least one digit ('0'-'9').";
            }
            if (passwordTxt.Text.All(char.IsLetterOrDigit))
            {
                errorPassword = errorPassword + "\n- At least one non alphanumeric character.";
            }

            if (errorPassword != "")
            {                
                responseTxt.Text = $"Password should meet these requirements: {errorPassword}";
                return;
            }




            var register = new Register()
            {
                Email = emailTxt.Text,
                UserName = usernameTxt.Text,
                Password = passwordTxt.Text
            };

            RestRequest request = new RestRequest("Authentication/Register").AddJsonBody(register);
            var response = await RestSharp.client.ExecutePostAsync(request);

            int numericStatusCode = (int)response.StatusCode;

            if (numericStatusCode == 200)
            {
                responseTxt.Foreground = Brushes.ForestGreen;
                responseTxt.Text = "User successfully created";

                emailTxt.Text = "";
                usernameTxt.Text = "";
                passwordTxt.Text = "";
                confirmPasswordTxt.Text = "";
            }

            else if (numericStatusCode == 400)
            {

                responseTxt.Text = JsonConvert.DeserializeObject<string>(response.Content);
            }

            else responseTxt.Text = "Server Error. Try again later.";

        }






    }
}
