using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Sample.DAL;
using Sample.DAL.Models;
using Sample.Extensions.DAL;
using Sample.Service.Configuration;
using Sample.Business.Services;
using Sample.Business.Services.Game;
using System.Text.Json;
using System.Text.Json.Serialization;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.CookiePolicy;
using Sample.Service.Middleware;

namespace Sample.Service
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
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }); ;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample.Service", Version = "v1" });
            });

            var connString = Configuration.GetConnectionString("DefaultConnection");
            if (connString == "InMemory")
            {
                services.AddDbContext<DbContext, SampleDbContext>(options =>
                    options.UseInMemoryDatabase("inmemory"));
            }
            else
            {
                services.AddDbContext<DbContext, SampleDbContext>(options =>
                    options.UseNpgsql(connString));
            }
           
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<SampleDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, SampleDbContext>(
                options =>
                {
                    options.Clients.AddIdentityServerSPA("src", builder =>
                    {
                        builder.WithRedirectUri("/authentication/login-callback");
                        builder.WithLogoutRedirectUri("/authentication/logout-callback");
                    });
                }
                );

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews(option=>
            {
                option.Filters.Add(new ExceptionFilter());
                option.Filters.Add(new ValidationFilter());
            });
            services.AddRazorPages();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.Unspecified;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.AddTransient(typeof(IAsyncCrudService<>), typeof(AsyncCrudService<>));
            services.AddTransient(typeof(IAsyncOrderedQueryService<>), typeof(AsyncOrderedQueryService<>));
            services.AddTransient<IBalanceService, BalanceService>();
            services.AddTransient<IOperationService, OperationService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IGameStrategy, DefaultGameStrategy>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var forwardedHeadersOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                RequireHeaderSymmetry = false
            };
            forwardedHeadersOptions.KnownNetworks.Clear();
            forwardedHeadersOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(forwardedHeadersOptions);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample.Service v1");
                c.RoutePrefix = "swagger/ui";
            });

            var connString = Configuration.GetConnectionString("DefaultConnection");
            if (connString != "InMemory")
            {
                app.InitializeDatabase();
            }
            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseCookiePolicy(new CookiePolicyOptions() {
                HttpOnly = HttpOnlyPolicy.None,
                MinimumSameSitePolicy = SameSiteMode.None,
                Secure = CookieSecurePolicy.Always
            });
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}