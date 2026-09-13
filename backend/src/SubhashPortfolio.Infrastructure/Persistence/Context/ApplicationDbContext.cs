using Microsoft.EntityFrameworkCore;
using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Domain.Common;
using SubhashPortfolio.Domain.Entities;

namespace SubhashPortfolio.Infrastructure.Persistence.Context;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IDateTimeService _dateTimeService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IDateTimeService dateTimeService) : base(options)
    {
        _dateTimeService = dateTimeService;
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<SocialLink> SocialLinks => Set<SocialLink>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Experience> Experiences => Set<Experience>();
    public DbSet<ExperienceResponsibility> ExperienceResponsibilities => Set<ExperienceResponsibility>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectFeature> ProjectFeatures => Set<ProjectFeature>();
    public DbSet<ProjectTech> ProjectTechs => Set<ProjectTech>();
    public DbSet<Education> Educations => Set<Education>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();

    IRepository<User> IUnitOfWork.Users => new Repositories.GenericRepository<User>(this);
    IRepository<Profile> IUnitOfWork.Profiles => new Repositories.GenericRepository<Profile>(this);
    IRepository<SocialLink> IUnitOfWork.SocialLinks => new Repositories.GenericRepository<SocialLink>(this);
    IRepository<Skill> IUnitOfWork.Skills => new Repositories.GenericRepository<Skill>(this);
    IRepository<Experience> IUnitOfWork.Experiences => new Repositories.GenericRepository<Experience>(this);
    IRepository<ExperienceResponsibility> IUnitOfWork.ExperienceResponsibilities => new Repositories.GenericRepository<ExperienceResponsibility>(this);
    IRepository<Project> IUnitOfWork.Projects => new Repositories.GenericRepository<Project>(this);
    IRepository<ProjectFeature> IUnitOfWork.ProjectFeatures => new Repositories.GenericRepository<ProjectFeature>(this);
    IRepository<ProjectTech> IUnitOfWork.ProjectTechs => new Repositories.GenericRepository<ProjectTech>(this);
    IRepository<Education> IUnitOfWork.Educations => new Repositories.GenericRepository<Education>(this);
    IRepository<ContactMessage> IUnitOfWork.ContactMessages => new Repositories.GenericRepository<ContactMessage>(this);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = _dateTimeService.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = _dateTimeService.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        => Database.BeginTransactionAsync(cancellationToken);

    public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is not null)
        {
            return Database.CommitTransactionAsync(cancellationToken);
        }
        return Task.CompletedTask;
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is not null)
        {
            return Database.RollbackTransactionAsync(cancellationToken);
        }
        return Task.CompletedTask;
    }
}