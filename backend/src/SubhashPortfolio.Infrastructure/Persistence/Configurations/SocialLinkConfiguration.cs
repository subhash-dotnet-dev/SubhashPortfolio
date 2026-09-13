using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubhashPortfolio.Domain.Entities;

namespace SubhashPortfolio.Infrastructure.Persistence.Configurations;

public class SocialLinkConfiguration : IEntityTypeConfiguration<SocialLink>
{
    public void Configure(EntityTypeBuilder<SocialLink> builder)
    {
        builder.ToTable("SocialLinks");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.DisplayName).IsRequired().HasMaxLength(100);
        builder.Property(s => s.Url).IsRequired().HasMaxLength(2048);
        builder.Property(s => s.IconClass).HasMaxLength(100);

        builder.HasOne(s => s.Profile)
            .WithMany(p => p.SocialLinks)
            .HasForeignKey(s => s.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(s => !s.IsDeleted);
    }
}