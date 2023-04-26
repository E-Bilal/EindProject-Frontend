using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontWPF.Models
{
    public class Tweet
    {

        public string userId { get; set; }
        public string Post { get; set; }
        public DateTime? currentTime { get; set; }

        //public ICollection<TweetLike> TweetLike { get; set; }
    }
}
