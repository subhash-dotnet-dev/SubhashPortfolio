using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubhashPortfolio.Domain.Entities;

namespace SubhashPortfolio.Infrastructure.Persistence.Configurations;

public class ContactMessageConfiguration : IEntityTypeConfiguration<ContactMessage>
{
    public void Configure(EntityTypeBuilder<ContactMessage> builder)
    {
        builder.ToTable("ContactMessages");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(256);
        builder.Property(c => c.Phone).HasMaxLength(20);
        builder.Property(c => c.Subject).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Message).IsRequired().HasMaxLength(2000);
        builder.Property(c => c.IpAddress).HasMaxLength(50);
        builder.Property(c => c.UserAgent).HasMaxLength(500);

        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}