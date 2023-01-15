﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebSignalR.Models;
using WebSignalR.Services;

namespace WebSignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string message, string userName)
        {
            await Clients.All.SendAsync("Receive", message, userName);
        }
        [Authorize(Roles = "admin")]
        public async Task Notify(string message, string userName)
        {
            await Clients.All.SendAsync("Receive", message, userName);
        }
    }
}