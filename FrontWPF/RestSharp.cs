using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FrontWPF
{
    public static class RestSharp
    {
        public static RestClient client = new RestClient("https://localhost:44354/api/");

       


    }
}
