using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Persistence
{
    public class ShopDbContext : IdentityDbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> optionsBuilder) : base(optionsBuilder)
        {

        }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(ci => ci.Id);
                entity.Property(c => c.Id)
    .               ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Category>(a =>
            {
                a.HasMany(c => c.Items)
                .WithOne(o => o.Category)
                .HasForeignKey(c => c.CategoryId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
