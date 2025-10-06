using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        var fixedDate = new DateTime(2025, 10, 5, 14, 0, 0, DateTimeKind.Utc);

        // Seed de Clientes com IDs fixos
        var customersToSeed = new List<Customer>();
        for (int i = 1; i <= 10; i++)
        {
            customersToSeed.Add(new Customer { Id = Guid.Parse($"00000000-0000-0000-0000-{i:000000000000}"), Name = $"Cliente {i}", Email = $"cliente{i}@email.com", Document = $"{i}{i}{i}44455566", CreatedAt = fixedDate });
        }
        modelBuilder.Entity<Customer>().HasData(customersToSeed);

        // Seed de Produtos com IDs fixos
        var productsToSeed = new List<Product>();
        for (int i = 1; i <= 20; i++)
        {
            productsToSeed.Add(new Product 
            { 
                Id = Guid.Parse($"11111111-1111-1111-1111-{i:000000000000}"), 
                Name = $"Produto {i}", 
                Sku = $"SKU-PROD-{i:000}", 
                Price = 10m * i,
                StockQty = 100, 
                IsActive = true, 
                CreatedAt = fixedDate
            });
        }
        modelBuilder.Entity<Product>().HasData(productsToSeed);
    }
}
