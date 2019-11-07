using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using scheduler.service.IService;
using scheduler.service.Services;
using scheduler.service.Common_Utilities;


using Microsoft.AspNetCore.HttpsPolicy;


using Microsoft.EntityFrameworkCore;


using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using scheduler.EF.Model;
using AutoMapper;
using scheduler.Repository.IRepositories;
using scheduler.Repository.Repositories;
using scheduler.Repository.Infrastructure;
using scheduler.EF.Model.Pub;

namespace webAppAnguler
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder().SetBasePath(environment.ContentRootPath).AddJsonFile("appsettings.json", true, true);
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IFileProvider>(
          new PhysicalFileProvider(
              Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
             .AddSessionStateTempDataProvider();
            services.AddSession();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            //  services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
          
            services.AddSingleton<IConfiguration>(Configuration);
            services.Configure<CustomConfiguration>(Configuration.GetSection("CustomConfiguration"));
            services.AddDbContext<LoginDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("InstruTrackContext")));
            services.AddDbContext<SchedulerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("InstruTrackContext")));
            services.AddDbContext<PubContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PubContext")));
            scheduler.service.Common_Utilities.Utilities.Configure(Configuration);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            Mapper.Initialize(cfg => cfg.AddProfile<Automapper>());
            services.AddAutoMapper();
            
            services.AddTransient<ICaltUserRepository, CaltUserRepository>();
            services.AddScoped<ICaltUserService, CaltUserService>();
            //services.AddTransient<IAppontmentRepository, AppontmentRepository>();
            services.AddTransient<IAppontmentService, AppontmentService>();

            services.AddScoped<IFeedBackRepository, FeedBackRepository>();
            services.AddTransient<IFeedBackService, FeedBackService>();


            //services.AddTransient<ICaltUserService, CaltUserService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                   // spa.UseProxyToSpaDevelopmentServer("https://localhost:44319");
                }
            });
        }
    }
}
