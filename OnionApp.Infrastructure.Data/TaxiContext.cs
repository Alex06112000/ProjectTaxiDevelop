using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionApp.Domain.Core;

namespace OnionApp.Infrastructure.Data
{
    public class TaxiContext: IdentityDbContext
    {

        public new DbSet<User> Users { get; set; }
        public DbSet<Taxist> Taxists { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Order>()
          .HasOne(a => a.Taxist).WithOne(b => b.GlobalOrders)
          .HasForeignKey<Taxist>(e => e.IdTaxist);
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Taxist>().ToTable("Orders");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>().HasKey(u => u.Id);
            modelBuilder.Entity<Taxist>().HasKey(u => u.IdTaxist);
            modelBuilder.Entity<Car>().HasKey(u => u.IdCar);
            modelBuilder.Entity<Order>().HasKey(u => u.IdTaxist);

        }
        public TaxiContext()
        { }
        public TaxiContext(DbContextOptions<TaxiContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
