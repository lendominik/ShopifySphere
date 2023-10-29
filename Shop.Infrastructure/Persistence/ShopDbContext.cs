using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Persistence
{
    public class ShopDbContext : IdentityDbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> optionsBuilder) : base(optionsBuilder)
        {

        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasOne(a => a.Payment)
                .WithOne(p => p.Cart)
                .HasForeignKey<Payment>(p => p.CartId);

            modelBuilder.Entity<Cart>(a =>
            {
                a.HasMany(c => c.CartItems)
                .WithOne(o => o.Cart)
                .HasForeignKey(c => c.CartId);
            });

            modelBuilder.Entity<CartItem>()
                .HasOne(a => a.Item)
                .WithOne(p => p.CartItem)
                .HasForeignKey<CartItem>(p => p.ItemId);

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
