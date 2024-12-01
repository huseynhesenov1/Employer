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
            // Masters Table
            modelBuilder.Entity<Master>(entity =>
            {
                entity.ToTable("Masters");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Surname).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ExperienceYear).IsRequired();
                entity.Property(e => e.IsActive).IsRequired();

                // Foreign Key for Service
                entity.HasOne<Service>()
                      .WithMany(s => s.Masters)
                      .HasForeignKey(e => e.ServiceId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Orders Table
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ClientName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ClientSurname).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ClientPhoneNumber).HasMaxLength(15);
                entity.Property(e => e.ClientEmail).HasMaxLength(200);
                entity.Property(e => e.Problem).IsRequired().HasMaxLength(500);
                entity.Property(e => e.IsActive).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                // Foreign Key for Master
                entity.HasOne<Master>()
                      .WithMany(m => m.Orders)
                      .HasForeignKey(e => e.MasterId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Foreign Key for Service
                entity.HasOne<Service>()
                      .WithMany(s => s.Orders)
                      .HasForeignKey(e => e.ServiceId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Services Table
            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Services");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.IsActive).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            base.OnModelCreating(modelBuilder);
        }


    }
}
