using Woodbrass.Models;

namespace Woodbrass.Data;

using Microsoft.EntityFrameworkCore;

public class WoodBrassDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<ProductCart> ProductCarts { get; set; }

    public WoodBrassDbContext(DbContextOptions<WoodBrassDbContext> options) 
        : base(options) 
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductCart>()
            .HasKey(pc => new { pc.ProductId, pc.CartId });

        modelBuilder.Entity<ProductCart>()
            .HasOne(pc => pc.Product)
            .WithMany(p => p.ProductCarts)
            .HasForeignKey(pc => pc.ProductId);

        modelBuilder.Entity<ProductCart>()
            .HasOne(pc => pc.Cart)
            .WithMany(c => c.ProductCarts)
            .HasForeignKey(pc => pc.CartId);

        base.OnModelCreating(modelBuilder);
    }
}