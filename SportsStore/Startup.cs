using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SportsStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace SportsStore
{
    public class Startup
    {
        IConfigurationRoot Configuration;
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json").Build();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                        Configuration["Data:SportStoreProducts:ConnectionString"]));

            services.AddDbContext<AppIdentityDbContext>(options => 
                options.UseSqlServer(
                    Configuration["Data:SportStoreIdentity:ConnectionString"]));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();//.AddDefaultTokenProviders();

            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();//UseIdentity(); - obsolete
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                        name: null,
                        template: "{category}/Page/{page:int}/",
                        defaults: new { Controller = "Product", Action = "List" }
                    );
                routes.MapRoute(
                        name: null,
                        template: "Page/{page:int}/",
                        defaults: new { Controller = "Product", Action = "List", page = 1 }
                    );
                routes.MapRoute(
                        name: null,
                        template: "{category}/",
                        defaults: new { Controller = "Product", Action = "List", page = 1 }
                    );
                routes.MapRoute(
                        name: null,
                        template: "",
                        defaults: new { Controller = "Product", Action = "List", page = 1 }
                    );
                routes.MapRoute(
                    name: null,
                    template: "{controller}/{action}/{id?}/");

                //routes.MapRoute(
                //    name: "pagination",
                //    template: "Products/Page/{page}/",
                //    defaults: new { Controller = "Product", action = "List" });

                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Product}/{action=List}/{id?}");
            });
            SeedData.EnsurePopulated(serviceProvider);
            IdentitySeedData.EnsurePopulated(serviceProvider);
        }
    }
}
