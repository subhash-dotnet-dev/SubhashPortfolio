using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubhashPortfolio.Domain.Entities;

namespace SubhashPortfolio.Infrastructure.Persistence.Configurations;

public class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.ToTable("Educations");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Degree).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Institution).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Board).HasMaxLength(200);
        builder.Property(e => e.GradeOrPercentage).HasMaxLength(50);
        builder.Property(e => e.Description).HasMaxLength(1000);

        builder.HasOne(e => e.Profile)
            .WithMany(p => p.Educations)
            .HasForeignKey(e => e.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}