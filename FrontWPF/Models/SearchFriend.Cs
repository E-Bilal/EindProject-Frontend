using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace FrontWPF.Models
{
    public class SearchFriend
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public string Status { get; set; }


        public string? PendingUserVisible { get 
            {
                if (Status == User.Id)
                {
                    return "Visible";
                }

                else return "Collapsed";
            } 
        }
        public string? PendingFriendVisible { get
            {
                if (Status != User.Id && Status != "Friends")
                {
                    return "Visible";
                }

                else return "Collapsed";
            }
        }
    
        public string? PendingAcceptedVisible { get 
            { 
                if (Status == "Friends")
                {
                    return "Visible";
                }
                else return "Collapsed";
            }
        }



        //public string? PendingUserVisible
        //{
        //    get
        //    {
        //        if (Status == User.Id)
        //        {
        //            PendingFriendVisible = "Collapsed";
        //            AcceptedVisible = "Collapsed";
        //            return "Visible";
        //        }
        //        else if (Status == "2")
        //        {
        //            PendingFriendVisible = "Collapsed";
        //            AcceptedVisible = "Visible";
        //            return "Collapsed";
        //        }

        //        else
        //        {
        //            PendingFriendVisible = "Visible";
        //            AcceptedVisible = "Collapsed";
        //            return "Collapsed";

        //        }
        //    }

        //    set { }
        //}

        //public string? PendingFriendVisible
        //{
        //    get
        //    {
        //        if (Status == User.Id)
        //        {
        //            PendingUserVisible = "Visible";
        //            AcceptedVisible = "Collapsed";
        //            return "Collapsed";
        //        }
        //        else if (Status == "2")
        //        {
        //            PendingUserVisible = "Collapsed";
        //            AcceptedVisible = "Visible";
        //            return "Collapsed";
        //        }

        //        else
        //        {
        //            PendingUserVisible = "Collapsed";
        //            AcceptedVisible = "Collapsed";
        //            return "Visible";

        //        }
        //    }
        //    set
        //    {

        //    }


        //}

        //public string? AcceptedVisible
        //{
        //    get
        //    {
        //        if (Status == User.Id)
        //        {
        //            PendingUserVisible = "Visible";
        //            PendingFriendVisible = "Collapsed";
        //            return "Collapsed";
        //        }
        //        else if (Status == "2")
        //        {
        //            PendingUserVisible = "Collapsed";
        //            PendingFriendVisible = "Collapsed";
        //            return "Visible";
        //        }

        //        else
        //        {
        //            PendingUserVisible = "Collapsed";
        //            PendingFriendVisible = "Visible";
        //            return "Collapsed";

        //        }
        //    }
        //    set { }
        //}


        public override string ToString()
        {
            return $"{Username}";
        }
    }

}
