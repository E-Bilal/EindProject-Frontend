using RestSharp.Authenticators;
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
using FrontWPF.Models;

namespace FrontWPF
{
    /// <summary>
    /// Interaction logic for ResetPasswordWindow.xaml
    /// </summary>
    public partial class ResetPasswordWindow : Window
    {

        static string mail;

        private class ResetPasswordModel
        {
            public string Password { get; set; }
            public string Email { get; set; }
            public string RecoveryToken { get; set; }
        }

        public ResetPasswordWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            resetPassSP.Visibility = Visibility.Collapsed;
            //requestTokenSP.Visibility= Visibility.Collapsed;
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



        private void RequestToken(object sender, RoutedEventArgs e)
        {
            responseEmailTxt.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#D7596D");
            var email = new EmailAddressAttribute();
            if (!email.IsValid(emailTxt.Text))
            {
                //MessageBox.Show("Please enter a valid email address");
                responseEmailTxt.Text = "Passwords are not the same try again";
                emailTxt.Text = "";
                return;
            }


            RestRequest request = new RestRequest($"Settings/GetRecoveryToken?email={emailTxt.Text}");
            RestSharp.client.ExecuteGet(request);
            mail = emailTxt.Text;
            resetPassSP.Visibility = Visibility.Visible;
            requestTokenSP.Visibility = Visibility.Collapsed;
        }

        private async void ResetPassword(object sender, RoutedEventArgs e)
        {
            // Password Validation

            string errorPassword = "";
            responsePassTxt.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#D7596D");
            if (passwordTxt.Text != confirmPasswordTxt.Text)
            {
                //MessageBox.Show("Passwords are not the same try again");
                responsePassTxt.Text = "Passwords are not the same try again";
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
                //MessageBox.Show($"Password should meet these requirements: {errorPassword}");
                responsePassTxt.Text = $"Password should meet these requirements: {errorPassword}";
                return;
            }

            var resetPassword = new ResetPasswordModel
            {
                Password = passwordTxt.Text,
                Email = mail,
                RecoveryToken = resetTokenTxt.Text
            };


            RestRequest request = new RestRequest("Settings/ResetPassword").AddJsonBody(resetPassword);
            var response = await RestSharp.client.ExecutePostAsync(request);
            MessageBox.Show(response.Content.ToString());

        }
    }
}
