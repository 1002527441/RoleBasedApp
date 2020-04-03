using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ASP.NET_Core_RazorStripe1.Models;

namespace ASP.NET_Core_RazorStripe1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>(b =>
            {
                b.Property(e => e.Status)
                    .HasConversion<string>();
            });

            // Customize the ASP.NET Identity model and override the defaults if needed. For example,
            // you can rename the ASP.NET Identity table names and more. Add your customizations
            // after calling base.OnModelCreating(builder);
        }
    }
}