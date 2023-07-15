using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Id
            builder.HasKey(x => x.Id);
            // builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // ProductCrawlType
            builder.Property(x => x.ProductCrawlType).IsRequired();

            // CreatedOn
            builder.Property(x => x.CreatedOn).IsRequired();

            // RequestedAmount
            builder.Property(x => x.RequestedAmount).IsRequired();

            // TotalFoundAmount
            builder.Property(x => x.TotalFoundAmount).IsRequired();

            // Relationships
            builder.HasMany(x => x.OrderEvents)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            builder.ToTable("Orders");
        }
    }
}
