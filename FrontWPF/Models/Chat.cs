using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FrontWPF.Models
{
    public class Chat
    {

        public string Message { get; set; }
        public string ApplicationUserId { get; set; }
        public string ApplicatiFriendId { get; set; }
        public DateTime? currentTime { get; set; }
        public string? Username { get; set; }




        public string? Timeformatted { get; set; }
        public string? VisibleUser
        {
            get
            {
                if (ApplicationUserId == User.Id)
                {
                    return "Collapsed";
                }

                else return "Visible";
            }
        }
        public string? VisibleFriend
        {
            get
            {
                if (ApplicationUserId == User.Id)
                {
                    
                    return "Visible";
                }

                else return "Collapsed";
            }



        }
        public string? UsernameVisible { get; set; }

    }
}
