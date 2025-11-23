using BusinessObject.FacadeService;
using DataAccessObject.Data;
using DataAccessObject.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Ultitity.Options;

namespace BilliardsManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            //Cloudinary
            builder.Services.Configure<CloudinaryOptions>(
                builder.Configuration.GetSection("Cloudinary")
            );
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IFacadeService, FacadeService>();
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Table}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
