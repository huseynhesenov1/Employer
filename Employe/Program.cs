using Employe.DAL;
using Employe.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Employe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            builder.Services.AddControllersWithViews();
            builder.Services.AddIdentity<AppUser,IdentityRole>(
                op =>
                {
                    op.Password.RequiredLength = 4;
                    op.User.RequireUniqueEmail = true;

                }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddDbContext<AppDbContext>(
               options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql")));
            var app = builder.Build();
            app.UseStaticFiles();
            app.UseAuthentication();


            app.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
         


            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
#region
/*
 
 INSERT INTO Services (Title, Description, IsActive, CreatedAt, UpdatedAt)
VALUES
('Elektrik T?mir Xidm?ti', 'Pe??kar elektrik t?miri v? xidm?tl?ri.', 1, GETDATE(), NULL),
('Santexnik Xidm?ti', 'Evl?rd? v? ofisl?rd? santexnik probleml?rinin h?lli.', 1, GETDATE(), NULL),
('?T D?st?k Xidm?ti', 'Komp?ter v? ??b?k? probleml?rinin h?lli ???n xidm?tl?r.', 1, GETDATE(), NULL),
('Avtomobil T?mir Xidm?ti', 'H?r n?v avtomobil t?mir i?l?ri.', 1, GETDATE(), NULL),
('Mebel Y???m? v? T?miri', 'Mebel y???m? v? t?miri ?zr? xidm?tl?r.', 1, GETDATE(), NULL);


INSERT INTO Masters (Name, Surname, Email, PhoneNumber, Username, ExperienceYear, IsActive, ServiceId)
VALUES 
('Ali', 'H?seynov', 'ali.huseynov@example.com', '+994501234567', 'alihuseynov', 5, 1, 1),
('Aysel', 'M?mm?dova', 'aysel.mammadova@example.com', '+994551234568', 'ayselm', 3, 1, 2),
('Kamran', '?smay?lov', 'kamran.ismayilov@example.com', '+994701234569', 'kamranism', 7, 1, 3),
('N?rmin', 'Quliyeva', 'nermin.quliyeva@example.com', '+994601234570', 'nerminq', 2, 1, 4),
('Tamerlan', '?liyev', 'tamerlan.aliyev@example.com', '+994401234571', 'tamerlana', 10, 1, 5);


INSERT INTO Orders (ClientName, ClientSurname, ClientPhoneNumber, ClientEmail, ServiceId, MasterId, Problem, IsActive, CreatedAt, UpdatedAt)
VALUES
('Elvin', 'Sad?qov', '+994507654321', 'elvin.sadiqov@example.com', 1, 1, 'Ev elektrik sistemind? q?sa qapanma.', 1, GETDATE(), NULL),
('Leyla', 'M?mm?dli', '+994551112233', 'leyla.mammadli@example.com', 2, 2, 'M?tb?x kran?nda su s?z?nt?s?.', 1, GETDATE(), NULL),
('Murad', '?liyev', '+994702233445', 'murad.aliyev@example.com', 3, 3, 'Komp?terd? proqram t?minat? problemi.', 1, GETDATE(), NULL),
('G?nay', 'Abdullayeva', '+994601234567', 'gunay.abdullayeva@example.com', 4, 4, 'M?h?rrikd?n qeyri-adi s?s g?lir.', 1, GETDATE(), NULL),
('S?m?d', 'M?mm?dov', '+994401223344', 'samed.mammadov@example.com', 5, 5, 'D?hliz mebeli y???lmal?d?r.', 1, GETDATE(), NULL);


select * from Orders
select * from Masters
select * from Services
 
 */
#endregion