using BethanysPieShopAdmin.Models.Repositories;
using BethanysPieShopAdmin.Models;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShopAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        { 

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<BethanysPieShopDbContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("BethanysPieShopAdminDbContextConnection")));

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IPieRepository, PieRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<BethanysPieShopDbContext>();
                //context.Database.EnsureCreated();
                DbInitializer.Seed(context);
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
