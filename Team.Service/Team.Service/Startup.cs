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
using Team.DAL;
using Team.DAL.Models;
using Team.Extensions.DAL;
using Team.Service.Configuration;

namespace Team.Service
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test.Service", Version = "v1" });
            });

            // services.AddScoped<DbContext, TeamDbContext>((sp) =>
            // {
            //     var contextOptions = new DbContextOptionsBuilder<TeamDbContext>()
            //                         .UseNpgsql(Configuration.GetConnectionString("PostgreSQL"))
            //                         .Options;
            //     return new TeamDbContext(contextOptions);
            // });
            services.AddDbContext<TeamDbContext>(options =>
                 options.UseNpgsql(Configuration.GetConnectionString("PostgreSQL")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<TeamDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, TeamDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();


            services.AddTransient(typeof(IAsyncCrudService<>), typeof(AsyncCrudService<>));
            services.AddTransient(typeof(IAsyncOrderedQueryService<>), typeof(AsyncOrderedQueryService<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test.Service v1"));

            app.InitializeDatabase();
            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
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