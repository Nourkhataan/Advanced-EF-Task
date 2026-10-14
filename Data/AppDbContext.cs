using EF_Task.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Task.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        { 
        }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name);

            modelBuilder.Entity<Product>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Category>().HasData(
       new Category { Id = 1, Name = "Electronics" },
       new Category { Id = 2, Name = "Books" },
       new Category { Id = 3, Name = "Clothing" }
   );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Price = 25000,
                    CategoryId = 1,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 2,
                    Name = "Smartphone",
                    Price = 15000,
                    CategoryId = 1,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 3,
                    Name = "C# Programming Book",
                    Price = 600,
                    CategoryId = 2,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 4,
                    Name = "T-Shirt",
                    Price = 350,
                    CategoryId = 3,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 5,
                    Name = "Headphones",
                    Price = 1200,
                    CategoryId = 1,
                    IsDeleted = false
                }
            );

        }



    }
}
