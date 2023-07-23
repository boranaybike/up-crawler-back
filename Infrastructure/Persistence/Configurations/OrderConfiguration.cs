using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Id
            builder.HasKey(x => x.Id);

            // ProductCrawlType
            builder.Property(x => x.ProductCrawlType).IsRequired();

            // CreatedOn
            builder.Property(x => x.CreatedOn).IsRequired();

            // RequestedAmount
            builder.Property(x => x.RequestedAmount).IsRequired();

            // TotalFoundAmount
            builder.Property(x => x.TotalFoundAmount).IsRequired();

            builder.ToTable("Orders");
        }
    }
}
