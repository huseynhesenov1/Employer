using Employe.Models;
using Microsoft.EntityFrameworkCore;

namespace Employe.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Master> Masters { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
               .HasOne(e => e.Master)
               .WithMany(e => e.Orders)
               .HasForeignKey(e => e.MasterId)
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Master>()
              .HasOne(e => e.Order)
              .WithMany(e => e.Masters)
              .HasForeignKey(e => e.MasterId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
