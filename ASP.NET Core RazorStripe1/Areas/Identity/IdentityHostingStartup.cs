﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ASP.NET_Core_RazorStripe1.Data;

[assembly: HostingStartup(typeof(ASP.NET_Core_RazorStripe1.Areas.Identity.IdentityHostingStartup))]
namespace ASP.NET_Core_RazorStripe1.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}