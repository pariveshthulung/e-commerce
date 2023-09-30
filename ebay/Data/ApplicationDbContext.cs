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
    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderItems> OrderItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().HasData(
            new Product { 
                id = 1,
                Name = "Iphone 11",
                Description="This is nice phone.",
                Price=10000,
                Quantity=100,
                Color="red",
                Brand="Iphone",
                CategoryId = 1
            },

            new Product {
                id = 2,
                Name = "SamSung Galaxy",
                Description = "This is nice Samsung.",
                Price = 50000,
                Quantity = 100,
                Color = "Green",
                Brand = "Samsung",
                CategoryId = 1
            },

            new Product { 
                id = 3, 
                Name = "PoCO X3", 
                Description = "This is nice POCO.", 
                Price = 30000, 
                Quantity = 100, 
                Color = "Blue", 
                Brand = "Poco",
                CategoryId = 1
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { id=1, Name="Electronics"},
            new Category { id=2, Name="Auto Parts"},
            new Category { id=3, Name="Softwares"},
            new Category { id=4, Name="Books"},
            new Category { id=5, Name="Sports"}
        );
        modelBuilder.Entity<Customer>().HasData(
            new Customer { id=1, FirstName ="Ram",LastName="Rai"},
            new Customer { id=2, FirstName ="Hari",LastName="Magar"},
            new Customer { id=3, FirstName ="Shyam",LastName="Limbu"}
        );

        modelBuilder.Entity<Order>().HasData(
            new Order { id=1, CustomerId=1},
            new Order { id=2, CustomerId=1},
            new Order { id=3, CustomerId=3},
            new Order { id=4, CustomerId=2}
        );

        modelBuilder.Entity<OrderItems>().HasData(
            new OrderItems { id=1, OrderDetailsId= 1 ,ProductId= 1},
            new OrderItems { id=2, OrderDetailsId= 2 ,ProductId= 4},
            new OrderItems { id=3, OrderDetailsId= 3 ,ProductId= 5},
            new OrderItems { id=4, OrderDetailsId= 4 ,ProductId= 6}
        );
    }
}

