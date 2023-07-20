using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

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

            builder.ToTable("SeleniumLogs");
        }
    }
}