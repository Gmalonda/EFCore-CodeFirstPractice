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
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(ConfigurationConst.ConnectionString);
        }

    }
}
