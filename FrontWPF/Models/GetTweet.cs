using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontWPF.Models
{
    public class GetTweet
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public string Post { get; set; }

        public DateTime currentTime { get; set; }
        public bool StatusLike { get; set; }

        public string Username { get; set; }

        public int? AmountLikes { get; set; }

        public string? LikeVisible
        {
            get
            {
                if (StatusLike)
                {
                    return "Collapsed";
                }

                else return "Visible";
            }
        }
        public string? DislikeVisible
        {
            get
            {
                if (StatusLike)
                {
                    return "Visible";
                }

                else return "Collapsed";
            }
        }
        public string? DeleteVisible
        {
            get
            {
                if (ApplicationUserId == User.Id || User.Role == "Admin" || User.Role == "SuperUser")
                {
                    return "Visible";
                }

                else return "Collapsed";
            }


        }
    }
} 
