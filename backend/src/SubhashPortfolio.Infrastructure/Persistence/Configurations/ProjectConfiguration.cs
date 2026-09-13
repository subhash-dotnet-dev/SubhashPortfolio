using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubhashPortfolio.Domain.Entities;

namespace SubhashPortfolio.Infrastructure.Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Subtitle).HasMaxLength(300);
        builder.Property(p => p.ShortDescription).IsRequired().HasMaxLength(1000);
        builder.Property(p => p.Problem).HasMaxLength(4000);
        builder.Property(p => p.Solution).HasMaxLength(4000);
        builder.Property(p => p.Architecture).HasMaxLength(4000);
        builder.Property(p => p.SecurityNotes).HasMaxLength(4000);
        builder.Property(p => p.EngineeringChallenges).HasMaxLength(4000);
        builder.Property(p => p.Results).HasMaxLength(4000);
        builder.Property(p => p.GithubUrl).HasMaxLength(2048);
        builder.Property(p => p.LiveDemoUrl).HasMaxLength(2048);
        builder.Property(p => p.ThumbnailUrl).HasMaxLength(2048);

        builder.HasOne(p => p.Profile)
            .WithMany(pr => pr.Projects)
            .HasForeignKey(p => p.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}

public class ProjectFeatureConfiguration : IEntityTypeConfiguration<ProjectFeature>
{
    public void Configure(EntityTypeBuilder<ProjectFeature> builder)
    {
        builder.ToTable("ProjectFeatures");
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Description).IsRequired().HasMaxLength(500);

        builder.HasOne(f => f.Project)
            .WithMany(p => p.Features)
            .HasForeignKey(f => f.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(f => !f.IsDeleted);
    }
}

public class ProjectTechConfiguration : IEntityTypeConfiguration<ProjectTech>
{
    public void Configure(EntityTypeBuilder<ProjectTech> builder)
    {
        builder.ToTable("ProjectTechs");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired().HasMaxLength(100);

        builder.HasOne(t => t.Project)
            .WithMany(p => p.Technologies)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(t => !t.IsDeleted);
    }
}