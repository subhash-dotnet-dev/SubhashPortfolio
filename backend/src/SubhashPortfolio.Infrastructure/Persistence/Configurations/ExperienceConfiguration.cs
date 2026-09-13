using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubhashPortfolio.Domain.Entities;

namespace SubhashPortfolio.Infrastructure.Persistence.Configurations;

public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.ToTable("Experiences");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Company).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Role).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Location).HasMaxLength(200);
        builder.Property(e => e.Description).HasMaxLength(2000);
        builder.Property(e => e.TechEnvironment).HasMaxLength(1000);

        builder.HasOne(e => e.Profile)
            .WithMany(p => p.Experiences)
            .HasForeignKey(e => e.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}

public class ExperienceResponsibilityConfiguration : IEntityTypeConfiguration<ExperienceResponsibility>
{
    public void Configure(EntityTypeBuilder<ExperienceResponsibility> builder)
    {
        builder.ToTable("ExperienceResponsibilities");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Description).IsRequired().HasMaxLength(500);

        builder.HasOne(r => r.Experience)
            .WithMany(e => e.Responsibilities)
            .HasForeignKey(r => r.ExperienceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(r => !r.IsDeleted);
    }
}