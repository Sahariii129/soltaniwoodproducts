using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoltaniWeb.Models.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SoltaniWeb.Hubs;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using SoltaniWeb.Models.utility;
using SoltaniWeb.Models.Services.Interfaces;
using SoltaniWeb.Models.Services;
using Microsoft.AspNetCore.Rewrite;
using Newtonsoft.Json.Serialization;
using SoltaniWeb.Models.Services.Company;
using SoltaniWeb.Models.Services.Person;
using AutoMapper;
using SoltaniWeb.Models.Services.ArchiveSms;
using SoltaniWeb.Models.Services.ArchiveSms.MapperProfile;
using SoltaniWeb.Models.Services.Company.MapperProfile;
using SoltaniWeb.Models.Services.Person.MapperProfile;

namespace SoltaniWeb
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
            services.AddDbContext<_4820_soltaniwebContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("Database")));

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromHours(3);
                options.Cookie.HttpOnly = true;
            });
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSignalR();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddKendo();
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                // Maintain property names during serialization. See:
                // https://github.com/aspnet/Announcements/issues/194
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddResponseCaching();
            #region Authentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(options =>
            {
                options.LoginPath = "/home/index2";
                options.LogoutPath = "/home/index2";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);

            });

            #endregion


            #region ioc
            services.AddTransient<IViewRenderService, RenderViewToString>();
            services.AddTransient<IProductsServices, ProductsServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IArchiveSmsService, ArchiveSmsService>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PersonProfile());
                mc.AddProfile(new CompanyProfile());
                mc.AddProfile(new ArchiveSmsProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseResponseCaching();
            app.UseAuthentication();

            //
            var options = new RewriteOptions();
            options.AddRedirectToWww();

            options.AddRedirectToHttps();
            app.UseRewriter(options);



            //


            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chatHub");

            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

  
}
