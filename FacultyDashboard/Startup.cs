using Common;
using FacultyDashboard.Hubs;
using FacultyDashboard.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Configuration;
using WebFramework.CustomMapping;
using WebFramework.Middlewares;

namespace FacultyDashboard
{
    public class Startup
    {
        private readonly SiteSettings _siteSetting;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // برای اپلود فایل
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.AddControllersWithViews();
            //services.AddRazorPages();
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.InitializeAutoMapper();
            services.AddDbContext(Configuration);

            services.AddMinimalMvc();

            services.AddCustomIdentity(_siteSetting.IdentitySettings);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/Login"; //"/Login/UserAccessDenied";
                //options.ExpireTimeSpan = TimeSpan.FromMinutes(43200); // 1month
                options.ExpireTimeSpan = TimeSpan.FromDays(31);
                options.Cookie.Name = "UserLoginCookie";

                //options.Cookie.Domain = options.CookieDomain;
                //options.SlidingExpiration = true;
                //options.ExpireTimeSpan = options.CookieLifetime;
                //options.TicketDataFormat = ticketFormat;
                //options.CookieManager = new CustomChunkingCookieManager();
            });
            services.AddSignalR(options => options.EnableDetailedErrors = true);

            services.AddCors(options =>
            {
                options.AddPolicy("WebSocketCors", builder =>
                {
                    builder.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });
            services.AddCustomApiVersioning();

            // استفاده از Autofac
            return services.BuildAutofacServiceProvider();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomExceptionHandler();

            app.UseHsts(env);

            //       app.UseElmah();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // برای احراز هویت این دوتا حتما باید قبل از mvc باشن
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<dataHub>("/dataHub");

                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapAreaControllerRoute(
                    name: "Education",
                    areaName: "Education",
                    pattern: "Education/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapAreaControllerRoute(
                    name: "IT",
                    areaName: "IT",
                    pattern: "IT/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapAreaControllerRoute(
                    name: "Research",
                    areaName: "Research",
                    pattern: "Research/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapAreaControllerRoute(
                    name: "Resource",
                    areaName: "Resource",
                    pattern: "Resource/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapAreaControllerRoute(
                    name: "Student",
                    areaName: "Student",
                    pattern: "Student/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllers();
                //endpoints.MapRazorPages();

                app.UseWebSockets(new Microsoft.AspNetCore.Builder.WebSocketOptions
                {
                    // Keep active intervals 
                    KeepAliveInterval = TimeSpan.FromMinutes(5),
                });
                //  Note that this is the point !!!!
                app.UseMiddleware<WebsocketHandlerMiddleware>();
                // Custom cross domain rules 
                app.UseCors("WebSocketCors");
            });
        }
    }
}
