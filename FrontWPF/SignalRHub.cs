﻿using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontWPF
{
    public static class SignalRHub
    {
        public static HubConnection connection { get; set; }
    }
}
