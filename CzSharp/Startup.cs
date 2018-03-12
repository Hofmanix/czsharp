using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using CzSharp.Model;
using CzSharp.Model.Entities;
using CzSharp.Model.Repositories;
using CzSharp.Model.Repositories.Blog;
using CzSharp.Model.Repositories.Forum;
using CzSharp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CzSharp
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
            services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(options
                => options
                    .UseLazyLoadingProxies()
                    .UseNpgsql(Configuration.GetConnectionString("DbPath")));

            services.AddIdentity<User, UserRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
                    
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(30);
                options.LoginPath = "/Users/Login";
                options.LogoutPath = "/Users/Logout";
                options.AccessDeniedPath = "/Users/AccessDenied";
                options.SlidingExpiration = true;
            });

            AddRepositories(services);
            AddServices(services);

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            AddPolices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en"), 
                new CultureInfo("en-US"),
                new CultureInfo("cs"),
                new CultureInfo("cs-CZ")
            };
            
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("cs"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            
            app.UseAuthentication();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Default}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            CreateRoles(serviceProvider).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var usersManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var rolesManager = serviceProvider.GetRequiredService<RoleManager<UserRole>>();

            string[] roleNames = UserRole.Roles;

            foreach (var role in roleNames)
            {
                if (!await rolesManager.RoleExistsAsync(role))
                {
                    await rolesManager.CreateAsync(new UserRole
                    {
                        Name = role
                    });
                }
            }

            if ((await usersManager.FindByNameAsync(Configuration["Administrator:UserName"])) == null)
            {
                var admin = new User
                {
                    UserName = Configuration["Administrator:UserName"],
                    Email = Configuration["Administrator:Email"],
                    EmailConfirmed = true
                };
                var creationResult = await usersManager.CreateAsync(admin, Configuration["Administrator:Password"]);
                if (creationResult.Succeeded)
                {
                    await usersManager.AddToRoleAsync(admin, UserRole.Administrator);
                }
            }
            
        }

        private void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IArticlesRepository, ArticlesRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<ICodeRepository, CodeRepository>();
            services.AddScoped<IEventsRepository, EventsRepository>();
            services.AddScoped<ITagsRepository, TagsRepository>();

            services.AddScoped<ITopicGroupsRepository, TopicGroupsRepository>();
            services.AddScoped<ITopicsRepository, TopicsRepository>();
            services.AddScoped<IDiscussionsRepository, DiscussionsRepository>();
            services.AddScoped<IContributionsRepository, ContributionsRepository>();
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<ITagsService, TagsService>();
        }

        private void AddPolices(IServiceCollection services)
        {
            services.AddAuthorization(options =>
                {
                    options.AddPolicy(Polices.Bloggers,
                        policy => policy.RequireRole(UserRole.Administrator, UserRole.SeniorBlogger, UserRole.Blogger));
                    options.AddPolicy(Polices.SeniorBloggers,
                        policy => policy.RequireRole(UserRole.Administrator, UserRole.SeniorBlogger));
                    options.AddPolicy(Polices.Coders, 
                        policy => policy.RequireRole(UserRole.Administrator, UserRole.Coder));
                    options.AddPolicy(Polices.EventCreators,
                        policy => policy.RequireRole(UserRole.Administrator, UserRole.EventCreator));
                    options.AddPolicy(Polices.Moderators,
                        policy => policy.RequireRole(UserRole.Administrator, UserRole.Moderator));
                });
        }
    }
}
