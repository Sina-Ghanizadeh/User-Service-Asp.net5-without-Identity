using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Data.Repositories.SendMail;
using UserService.Data.Repositories.Users;
using UserService.Models;

namespace UserService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            #region Db Context

            services.AddDbContext<DatabaseContext>(options =>
            {

                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });


            #endregion

            #region IOC
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IMailService, MailService>();
            #endregion
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(option =>
               {

                   option.LoginPath = "/Account/Login";
                   option.LogoutPath = "/Account/Logout";
                   option.ExpireTimeSpan = TimeSpan.FromDays(10);
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Use(async (context, next) =>
            {

                if (context.Request.Path.StartsWithSegments("/DashBoard"))
                {
                    if (!context.User.Identity.IsAuthenticated)
                    {
                        context.Response.Redirect("/Account/Login");
                    }

                }
                await next.Invoke();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
