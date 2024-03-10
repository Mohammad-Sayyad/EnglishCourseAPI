using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TheListener.Models;
namespace TheListener.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
              new Category { CategoryId = 1, CategoryName = "Germany" },
              new Category { CategoryId = 2, CategoryName = "French" },
              new Category { CategoryId = 3, CategoryName = "English" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Listen German",
                    Description = "This is Jana and welcome to Storynory. I’m back with another cheeky Monkey story.",
                    ImageUrl = "sssssssssssss",
                    CategoryId = 1
                }, new Product
                {
                    Id = 2,
                    Name = "Listen German 2",
                    Description = "Katie is learning Latin - the language of the ancient Romans",
                    ImageUrl = "sssssssssssss",
                    CategoryId = 1
                }, new Product
                {
                    Id = 3,
                    Name = "Listen French",
                    Description = "Jana talks with our friends Matthew and Elaine Sweetapple, the creators of Lost on Infinity",
                    ImageUrl = "sssssssssssss",
                    CategoryId = 2
                }, new Product
                {
                    Id = 4,
                    Name = "Listen French 2",
                    Description = "This year Chinese and many Asian nations are celebrating the year of the Dragon.",
                    ImageUrl = "sssssssssssss",
                    CategoryId = 2
                }, new Product
                {
                    Id = 5,
                    Name = "Listen English",
                    Description = "Parents and older kids check out Relaxivity and our Cleopatra story.",
                    ImageUrl = "sssssssssssss",
                    CategoryId = 3
                }, new Product
                {
                    Id = 6,
                    Name = "Listen English 2",
                    Description = "Jana, and I’m here with a story about an old friend of ours. His name is Birdy.",
                    ImageUrl = "sssssssssssss",
                    CategoryId = 3
                }


                );
        }

    }
}
