using Common;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacultyDashboard.Hubs
{
    public class dataHub : Hub
    {
        public async Task GetNotification(string message)
        {
            await Clients.All.SendAsync("newMessage", message);
        }
    }
}