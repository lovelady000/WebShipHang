﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ShipShop.Web.Hubs
{
    public class OrderHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void NewOrder(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
    }
}