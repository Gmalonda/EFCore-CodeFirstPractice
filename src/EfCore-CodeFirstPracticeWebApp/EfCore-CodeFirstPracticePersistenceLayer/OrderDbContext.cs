using EfCore_CodeFirstPracticePersistenceLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EfCore_CodeFirstPracticePersistenceLayer
{
    public class OrderDbContext :  DbContext
    {

        public OrderDbContext()
        {

        }
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
           : base(options)
        {
           
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
           .Property(b => b.FirstName).HasColumnName("CustomerFirstName").HasMaxLength(50);
            
            modelBuilder.Entity<Customer>().ToTable("CustomerWithOrder");

            modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey("FK_CustomerID").HasConstraintName("FK_Orders_CustomerWithOrder_FK_CustomerID");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(ConfigurationConst.ConnectionString);

        }

    }
}
