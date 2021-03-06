using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazorApp15.Areas.Identity;
using BlazorApp15.Data;
using BlazorStrap;
using Microsoft.AspNetCore.Http;
using Blazored.LocalStorage;
using System.Net.Http;
using Elsa.Activities.Http.Extensions;
using Elsa.Activities.Timers.Extensions;
using Microsoft.EntityFrameworkCore.Design;
using System.Text.Json;

using Jint;
using System.Runtime;
using Elsa.Persistence.EntityFrameworkCore.Extensions;

namespace BlazorApp15
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {


            

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBootstrapCss();

            services.AddMvc(); 

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<NorthwindContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("NorthwindConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()             
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddSession();

            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddSingleton<WeatherForecastService>();

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(opt => { opt.LoginPath = new PathString("/Home/Index/"); });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddProtectedBrowserStorage();
            services.AddScoped<StorageServices>();
            services.AddScoped<NorthwindService>();

            services.AddElsa()                   
                .AddHttpActivities()
                .AddTimerActivities();



            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpActivities();

            app.UseHttpsRedirection();     

            app.UseStaticFiles();

            app.UseCookiePolicy();
   
            app.UseSession();
      

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
