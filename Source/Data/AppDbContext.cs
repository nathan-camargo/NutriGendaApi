using Microsoft.EntityFrameworkCore;

namespace NutriGendaApi.src.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<HealthProfile> HealthProfiles { get; set; }
        public DbSet<Diet> Diets { get; set; }
        public DbSet<Nutritionist> Nutritionists { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User and Nutritionist Relationship
            modelBuilder.Entity<User>()
                .HasOne<Nutritionist>()
                .WithMany()
                .HasForeignKey(u => u.NutritionistToken)
                .HasPrincipalKey(n => n.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // HealthProfile to User Relationship
            modelBuilder.Entity<HealthProfile>()
                .HasOne(hp => hp.User)
                .WithOne(u => u.HealthProfile)
                .HasForeignKey<HealthProfile>(hp => hp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Diet to User Relationship
            modelBuilder.Entity<Diet>()
                .HasOne(d => d.User)
                .WithMany(u => u.Diets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Custom configurations for Nutritionist
            modelBuilder.Entity<Nutritionist>()
                .Property(n => n.Email)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}

