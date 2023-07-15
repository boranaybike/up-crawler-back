using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.Configurations.Application;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderEvent> OrderEvents { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configurations
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // I used these because I do not want to fill the order id automatically.
            modelBuilder.ApplyConfiguration(new OrderConfiguration());

            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.ApplyConfiguration(new OrderEventConfiguration());

            // Ignores

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Gerekirse burada bazı iş mantığı eklenebilir

            return await base.SaveChangesAsync(cancellationToken);
        }

    }

}
