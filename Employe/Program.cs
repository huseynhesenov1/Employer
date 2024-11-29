using Employe.DAL;
using Microsoft.EntityFrameworkCore;

namespace Employe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<AppDbContext>(
               options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql")));
            var app = builder.Build();
            app.UseStaticFiles();


            app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
