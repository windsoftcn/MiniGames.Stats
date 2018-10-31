using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniGames.Stats.Data;
using MiniGames.Stats.Entities;
using MiniGames.Stats.Providers;
using MiniGames.Stats.Services;
using StackExchange.Redis;

namespace MiniGames.Stats
{
    public class Startup
    {
        private readonly IHostingEnvironment env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMemoryCache();

            services.AddDbContext<GameDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer_GameDb")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = true;
            }).AddEntityFrameworkStores<GameDbContext>()
            .AddDefaultTokenProviders();

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisDb")));
            services.AddTransient<IRedisProvider, RedisProvider>();
            services.AddTransient<IGameAppService, GameAppService>();
            services.AddTransient<IGameUserService, GameUserService>();


            services.AddMvc(options=>
            {
                options.RespectBrowserAcceptHeader = true;
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                // 增加此项也会屏蔽Api访问
                // options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            })
            .AddJsonOptions(options=>
            {
                 
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext =  scope.ServiceProvider.GetRequiredService<GameDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
