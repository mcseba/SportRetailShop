using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sport_Retail.Middlewares;
using Sport_Retail.Models;

namespace Sport_Retail
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddTransient<IProductRepository, EfProductRepository>();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration["Data:SportRetailProducts:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API v1");
                c.RoutePrefix = "api";
            });

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseMiddleware<ElapsedTimeMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Product List",
                    pattern: "{controller=Product}/{action=ListAll}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Product sorted",
                    pattern: "{controller=Product}/{action=List}/{category?}");
                
                endpoints.MapControllerRoute(
                    name: "Admin dashboard",
                    pattern: "{controller=Admin}/{action=Index}");
                
                endpoints.MapControllerRoute(
                    name: "Admin delete",
                    pattern: "{controller=Admin}/{action=Delete}/{id?}");
                
                endpoints.MapControllerRoute(
                    name: "Admin edit",
                    pattern: "{controller=Admin}/{action=Edit}/{id?}");

            });

        }
    }
}
