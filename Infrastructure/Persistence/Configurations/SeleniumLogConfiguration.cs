using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class SeleniumLogConfiguration : IEntityTypeConfiguration<SeleniumLog>
    {
        public void Configure(EntityTypeBuilder<SeleniumLog> builder)
        {
            // Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Message
            builder.Property(x => x.Message).IsRequired();

            // SentOn
            builder.Property(x => x.SentOn).IsRequired();

            builder.HasOne(o => o.Order).WithMany(s => s.SeleniumLogs).HasForeignKey(o => o.OrderId);


            builder.ToTable("SeleniumLogs");
        }
    }
}