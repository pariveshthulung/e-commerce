using ebay.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ebay.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().HasData(
            new Product { id = 1, Name = "Iphone", Description="This is nice phone.",Price=10000,Quantity=100,Color="red"},

            new Product { id = 2, Name = "SamSung", Description = "This is nice Samsung.", Price = 50000, Quantity = 100, Color = "Green" },

            new Product { id = 3, Name = "PoCO", Description = "This is nice POCO.", Price = 30000, Quantity = 100, Color = "Blue" }
            );
    }
}

