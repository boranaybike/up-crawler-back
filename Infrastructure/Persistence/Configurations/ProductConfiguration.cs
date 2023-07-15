using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Application
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // ID
            builder.HasKey(x => x.Id);

            // Name
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(150);
            builder.HasIndex(x => x.Name);

            // Picture
            builder.Property(x => x.Picture).IsRequired();
            builder.Property(x => x.Picture).HasMaxLength(150);

            // IsOnSale
            builder.Property(x => x.IsOnSale).IsRequired();

            // Price
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(11,8)");

            // SalePrice
            builder.Property(x => x.SalePrice).IsRequired();
            builder.Property(x => x.SalePrice).HasColumnType("decimal(11,8)");

            builder.HasOne(p => p.Order).WithMany(o => o.Products).HasForeignKey(p => p.OrderId);

            builder.ToTable("Products");
        }
    }
}
