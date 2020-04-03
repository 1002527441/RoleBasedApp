using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ASP.NET_Core_RazorStripe1.Models;

namespace ASP.NET_Core_RazorStripe1.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //TODO: remove code for migrations
            context.Database.EnsureCreated();

        }
    }
}