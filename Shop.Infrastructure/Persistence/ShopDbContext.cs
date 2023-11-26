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
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Cart>()
            //    .HasOne(a => a.Payment)
            //    .WithOne(p => p.Cart)
            //    .HasForeignKey<Payment>(p => p.CartId);

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasMany(i => i.CartItems)
                    .WithOne(ci => ci.Item)    
                    .HasForeignKey(ci => ci.ItemId);
            });


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
