using FrontWPF.Models;
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
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace FrontWPF.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();

            usernameTxt.Text = User.UserName;
            emailTxt.Text = User.Email;
            roleTxt.Text = User.Role;

            if (User.Role == "User" && User.RoleRequest == "NoRequest")
            {
                requestRoleBtn.Visibility = Visibility.Visible;
            }

            else if (User.Role == "User" && User.RoleRequest == "Pending")
            {
                pendingTxt.Visibility = Visibility.Visible;
            }

            else if (User.Role == "Admin" && User.RoleRequest == "Accepted")
            {
                revokeRoleBtn.Visibility = Visibility.Visible;
            }

        }

        private void EditUsername(object sender, RoutedEventArgs e)
        {
            usernameTxt.IsEnabled = !usernameTxt.IsEnabled;
            changeUsernameBtn.Visibility = Visibility.Visible;
            editUsernameBtn.Visibility = Visibility.Collapsed;
        }
        private async void ChangeUsername(object sender, RoutedEventArgs e)
        {
            string errorUsername = "";
            responseUsernameTxt.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#D7596D");

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
                responseUsernameTxt.Text = $"Username should meet these requirements: {errorUsername}";
                return;
            }



            var changeUsername = new ChangeUsername()
            {
                Id = User.Id,
                NewUsername= usernameTxt.Text

            };

            RestSharp.client.Authenticator = new JwtAuthenticator(User.Token);
            RestRequest request = new RestRequest("Settings/ChangeUsername").AddJsonBody(changeUsername);

            try
            {

                var response = await RestSharp.client.PatchAsync(request);

                User.UserName = JsonConvert.DeserializeObject<string>(response.Content.ToString());
                usernameTxt.Text = User.UserName;

                responseUsernameTxt.Foreground = Brushes.ForestGreen;
                responseUsernameTxt.Text = "Username successfully updated.";

                changeUsernameBtn.Visibility = Visibility.Collapsed;
                editUsernameBtn.Visibility = Visibility.Visible;
                usernameTxt.IsEnabled = !usernameTxt.IsEnabled;
            }
            catch
            {
                responseUsernameTxt.Text = "Something went wrong, try again later.";
            }

        }


        private void EditEmail(object sender, RoutedEventArgs e)
        {
            emailTxt.IsEnabled = !emailTxt.IsEnabled;
            changeEmailBtn.Visibility = Visibility.Visible;
            editEmailBtn.Visibility = Visibility.Collapsed;

        }
        private async void ChangeEmail(object sender, RoutedEventArgs e)
        {

            var email = new EmailAddressAttribute();
            responseEmailTxt.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#D7596D");

            

            if (!email.IsValid(emailTxt.Text))
            {                
                responseEmailTxt.Text = "Please enter a valid email address";                
                return;
            }

            var changeUsername = new ChangeEmail()
            {
                Id = User.Id,
                NewEmail = emailTxt.Text

            };

            RestSharp.client.Authenticator = new JwtAuthenticator(User.Token);
            RestRequest request = new RestRequest("Settings/ChangeEmail").AddJsonBody(changeUsername);
            

            try
            {
                var response = await RestSharp.client.PatchAsync(request);
                User.Email = JsonConvert.DeserializeObject<string>(response.Content.ToString());
                emailTxt.Text = User.Email;
                responseEmailTxt.Foreground = Brushes.ForestGreen;
                responseEmailTxt.Text = "Email successfully updated.";

                changeEmailBtn.Visibility = Visibility.Collapsed;
                editEmailBtn.Visibility = Visibility.Visible;
                emailTxt.IsEnabled = !emailTxt.IsEnabled;

            }

            catch
            {
                responseEmailTxt.Text = "Something went wrong, try again later.";
            }

        }


        
        private void EditPassword(object sender, RoutedEventArgs e)
        {
            passdock.Visibility = Visibility.Collapsed;            
            changePassPanel.Visibility = Visibility.Visible;

            currentPassTxt.IsEnabled = !currentPassTxt.IsEnabled;
            newPassTxt.IsEnabled = !newPassTxt.IsEnabled;
            
        }
        private async void ChangePassword(object sender, RoutedEventArgs e)
        {

            
            string errorPassword = "";
            responseUsernameTxt.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#D7596D");

            if (newPassTxt.Text.Length < 6)
            {
                errorPassword = errorPassword + "\n- 6 Characters long ";
            }
            if (!newPassTxt.Text.Any(char.IsUpper))
            {
                errorPassword = errorPassword + "\n- At least one uppercase ('A'-'Z').";
            }
            if (!newPassTxt.Text.Any(char.IsDigit))
            {
                errorPassword = errorPassword + "\n- At least one digit ('0'-'9').";
            }
            if (newPassTxt.Text.All(char.IsLetterOrDigit))
            {
                errorPassword = errorPassword + "\n- At least one non alphanumeric character.";
            }

            if (errorPassword != "")
            {
                responsePassTxt.Text = $"Password should meet these requirements: {errorPassword}";
                return;
            }


            var changePassword = new ChangePassword()
            {
                Id = User.Id,
                CurrentPassword = currentPassTxt.Text,
                NewPassword = newPassTxt.Text,
            };

            
            RestSharp.client.Authenticator = new JwtAuthenticator(User.Token);
            
            var request = new RestRequest("Settings/ChangePassword").AddJsonBody(changePassword);

            try
            {
                await RestSharp.client.PatchAsync(request);

                responsePassTxt.Foreground = Brushes.ForestGreen;
                responsePassTxt.Text = "Password successfully updated.";

                passdock.Visibility = Visibility.Visible;
                changePassPanel.Visibility = Visibility.Collapsed;

                currentPassTxt.Text = "";
                newPassTxt.Text = "";
                
            }

            catch
            {
                responsePassTxt.Text = "Something went wrong, try again later.";
            }            
        }   


        private void DeleteAccount(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure that you want to delete you account?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                RestSharp.client.Authenticator = new JwtAuthenticator(User.Token);
                RestRequest request = new RestRequest($"Settings/DeleteAccount?id={User.Id}");

                try
                {

                    var response = RestSharp.client.Delete(request);
                    MessageBox.Show("Account Has successfuly been deleted you will be now sent to the Login Window.");

                    User.Id = "";
                    User.Email = "";
                    User.UserName = "";
                    User.Role = "";
                    User.RoleRequest = "";

                    var loginWindow = new LoginWindow();
                    loginWindow.Show();
                    var mainWindow = Window.GetWindow(this);
                    mainWindow.Close();
                }
                                

                catch
                {
                     MessageBox.Show("Something went wrong , try again later");

                }
            }
           
        }


        private async void RequestAdmin(object sender, RoutedEventArgs e)
        {
            responseRoleTxt.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#D7596D");

            RestSharp.client.Authenticator = new JwtAuthenticator(User.Token);
            RestRequest request = new RestRequest($"Settings/RequestAdminRole").AddQueryParameter("id",User.Id);

            try
            {
                var response = await RestSharp.client.PatchAsync(request);
                responseRoleTxt.Foreground = Brushes.ForestGreen;
                responseRoleTxt.Text = "Admin role successfully requested.";

                User.RoleRequest = JsonConvert.DeserializeObject<string>(response.Content.ToString());
                pendingTxt.Visibility = Visibility.Visible;
                requestRoleBtn.Visibility = Visibility.Collapsed;
            }

            catch
            {
                responseRoleTxt.Text = "Something went wrong , try again later.";
            }
        }

        private async void RevokeAdmin(object sender, RoutedEventArgs e)
        {

            responseRoleTxt.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#D7596D");
            RestSharp.client.Authenticator = new JwtAuthenticator(User.Token);           
            RestRequest request = new RestRequest($"Settings/RevokeAdminRole").AddQueryParameter("id", User.Id);


            try
            {
                var response = await RestSharp.client.PatchAsync(request);
                responseRoleTxt.Foreground = Brushes.ForestGreen;
                responseRoleTxt.Text = "Admin role successfully revoked.";

                User.Role = "User";
                User.RoleRequest = JsonConvert.DeserializeObject<string>(response.Content.ToString());
                
                revokeRoleBtn.Visibility = Visibility.Collapsed;
                requestRoleBtn.Visibility = Visibility.Visible;
                roleTxt.Text = "User";
            }

            catch
            {                
                responseRoleTxt.Text = "Something went wrong , try again later.";
            }


        }

    }
}
