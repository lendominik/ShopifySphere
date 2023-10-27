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
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(a => a.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId);

            modelBuilder.Entity<Order>(a =>
            {
                a.HasMany(c => c.OrderItems)
                .WithOne(o => o.Order)
                .HasForeignKey(c => c.OrderId);
            });

            modelBuilder.Entity<OrderItem>()
                .HasOne(a => a.Item)
                .WithOne(p => p.OrderItem)
                .HasForeignKey<OrderItem>(p => p.ItemId);

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
