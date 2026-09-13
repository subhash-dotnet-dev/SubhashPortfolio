using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubhashPortfolio.Domain.Entities;

namespace SubhashPortfolio.Infrastructure.Persistence.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profiles");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FullName).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Email).IsRequired().HasMaxLength(256);
        builder.Property(p => p.Phone).IsRequired().HasMaxLength(20);
        builder.Property(p => p.Location).IsRequired().HasMaxLength(200);
        builder.Property(p => p.ProfileImageUrl).HasMaxLength(2048);
        builder.Property(p => p.ResumeUrl).HasMaxLength(2048);
        builder.Property(p => p.ShortBio).IsRequired().HasMaxLength(500);
        builder.Property(p => p.ProfessionalSummary).IsRequired().HasMaxLength(2000);
        builder.Property(p => p.CareerDirection).IsRequired().HasMaxLength(500);
        builder.Property(p => p.CreatedBy).HasMaxLength(100);
        builder.Property(p => p.UpdatedBy).HasMaxLength(100);

        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}