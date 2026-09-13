using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Domain.Entities;
using SubhashPortfolio.Infrastructure.Persistence.Context;

namespace SubhashPortfolio.Infrastructure.Persistence.Seed;

public static class UserSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IDateTimeService dateTimeService)
    {
        if (context.Users.Any()) return;

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = "Subhash Yadav",
            Email = "subhash.dev79@gmail.com",
            PasswordHash = passwordHasher.Hash("Subhash@2026"),
            Role = "Admin",
            IsActive = true,
            CreatedAt = dateTimeService.UtcNow
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }
}