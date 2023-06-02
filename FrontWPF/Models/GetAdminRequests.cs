using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontWPF.Models
{
    public class GetAdminRequests
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string RoleRequest { get; set;}

        public string? Pending { get 
            {
                if (RoleRequest == "Pending")
                {
                    return "Visible";

                }

                else return "Collapsed";
            } 
        }

        public string? Accepted
        {
            get
            {
                if (RoleRequest == "Accepted")
                {
                    return "Visible";

                }

                else return "Collapsed";
            }
        }
    }
}
