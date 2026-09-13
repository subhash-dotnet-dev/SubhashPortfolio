using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Domain.Entities;
using SubhashPortfolio.Domain.Enums;
using SubhashPortfolio.Infrastructure.Persistence.Context;

namespace SubhashPortfolio.Infrastructure.Persistence.Seed;

public static class SocialLinkSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        Guid profileId,
        IDateTimeService dateTimeService)
    {
        if (context.SocialLinks.Any()) return;

        var links = new List<SocialLink>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Platform = SocialPlatform.GitHub,
                DisplayName = "GitHub",
                Url = "https://github.com/subhashyadav",
                IconClass = "fab fa-github",
                DisplayOrder = 1,
                IsVisible = true,
                ProfileId = profileId,
                CreatedAt = dateTimeService.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Platform = SocialPlatform.LinkedIn,
                DisplayName = "LinkedIn",
                Url = "https://www.linkedin.com/in/subhashyadav",
                IconClass = "fab fa-linkedin",
                DisplayOrder = 2,
                IsVisible = true,
                ProfileId = profileId,
                CreatedAt = dateTimeService.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Platform = SocialPlatform.LeetCode,
                DisplayName = "LeetCode",
                Url = "https://leetcode.com/subhashyadav",
                IconClass = "fas fa-code",
                DisplayOrder = 3,
                IsVisible = true,
                ProfileId = profileId,
                CreatedAt = dateTimeService.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Platform = SocialPlatform.HackerRank,
                DisplayName = "HackerRank",
                Url = "https://hackerrank.com/subhashyadav",
                IconClass = "fab fa-hackerrank",
                DisplayOrder = 4,
                IsVisible = true,
                ProfileId = profileId,
                CreatedAt = dateTimeService.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Platform = SocialPlatform.Instagram,
                DisplayName = "Instagram",
                Url = "https://instagram.com/subhash.yadav",
                IconClass = "fab fa-instagram",
                DisplayOrder = 5,
                IsVisible = true,
                ProfileId = profileId,
                CreatedAt = dateTimeService.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Platform = SocialPlatform.Facebook,
                DisplayName = "Facebook",
                Url = "https://facebook.com/subhash.yadav",
                IconClass = "fab fa-facebook",
                DisplayOrder = 6,
                IsVisible = true,
                ProfileId = profileId,
                CreatedAt = dateTimeService.UtcNow
            }
        };

        await context.SocialLinks.AddRangeAsync(links);
        await context.SaveChangesAsync();
    }
}