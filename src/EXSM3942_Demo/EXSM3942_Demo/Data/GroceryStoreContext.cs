﻿using EXSM3942_Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXSM3942_Demo.Data
{
    public class GroceryStoreContext : DbContext
    {
        public GroceryStoreContext() { }
        // This version of the construtor is used less frequently for customizing how the context is initialized.
        //public GroceryStoreContext(DbContextOptions<GroceryStoreContext> options) : base(options) { }

        // Each of these DbSets represents every record in their associated table. 
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }

        // Configures the context.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // If the context is not already configured:
            if (!optionsBuilder.IsConfigured)
            {
                // Specify the connection to the database.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=exsm3942_grocerystore", new MySqlServerVersion(new Version(10, 4, 24)));
            }
        }
        // Setup instructions for creating a model object.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                // Setup instructions for Product.
                entity.HasIndex(model => model.CategoryID).HasName($"FK_{nameof(Product)}_{nameof(ProductCategory)}");
                entity.Property(model => model.Name)
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");
                entity.Property(model => model.Description)
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasOne(x => x.ProductCategory).WithMany(y => y.Products)
                .HasForeignKey(x => x.CategoryID).HasConstraintName($"FK_{nameof(Product)}_{nameof(ProductCategory)}").OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<ProductCategory>(entity =>
            {
                // Setup instructions for ProductCategory.
                entity.Property(model => model.Name)
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");
                entity.Property(model => model.Description)
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");
            });
        }
    }
}
