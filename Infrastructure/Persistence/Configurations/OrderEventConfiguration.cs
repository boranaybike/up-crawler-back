using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderEventConfiguration : IEntityTypeConfiguration<OrderEvent>
    {
        public void Configure(EntityTypeBuilder<OrderEvent> builder)
        {
            // Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // OrderId
            builder.Property(x => x.OrderId).IsRequired();

            // Status
            builder.Property(x => x.Status).IsRequired();

            // CreatedOn
            builder.Property(x => x.CreatedOn).IsRequired();

            // Relationships
            builder.HasOne(x => x.Order)
                .WithMany(x => x.OrderEvents)
                .HasForeignKey(x => x.OrderId);

            builder.ToTable("OrderEvents");
        }
    }
}