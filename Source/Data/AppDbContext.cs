using Microsoft.EntityFrameworkCore;
using NutriGendaApi.Source.Models;

namespace NutriGendaApi.Source.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<HealthProfile> HealthProfiles { get; set; }
        public DbSet<Diet> Diets { get; set; }
        public DbSet<Nutritionist> Nutritionists { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }

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

            // Week to Diet Relationship
            modelBuilder.Entity<Week>()
                .HasOne(w => w.Diet)
                .WithMany(d => d.Weeks)
                .HasForeignKey(w => w.DietId)
                .OnDelete(DeleteBehavior.Cascade);

            // Meal to Week Relationship
            modelBuilder.Entity<Meal>()
                .HasOne(m => m.Week)
                .WithMany(w => w.Meals)
                .HasForeignKey(m => m.WeekId)
                .OnDelete(DeleteBehavior.Cascade);

            // FoodItem to Meal Relationship
            modelBuilder.Entity<FoodItem>()
                .HasOne(f => f.Meal)
                .WithMany(m => m.FoodItems)
                .HasForeignKey(f => f.MealId)
                .OnDelete(DeleteBehavior.Cascade);

            // Custom configurations for Nutritionist
            modelBuilder.Entity<Nutritionist>()
                .Property(n => n.Email)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
