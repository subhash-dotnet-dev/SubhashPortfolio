using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Domain.Entities;
using SubhashPortfolio.Infrastructure.Persistence.Context;

namespace SubhashPortfolio.Infrastructure.Persistence.Seed;

public static class ExperienceSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        Guid profileId,
        IDateTimeService dateTimeService)
    {
        if (context.Experiences.Any()) return;

        // 1. KGE Technologies Internship
        var kge = new Experience
        {
            Id = Guid.NewGuid(),
            Company = "KGE Technologies Pvt. Ltd.",
            Role = ".NET MVC Intern",
            Location = "Hyderabad",
            StartDate = new DateTime(2026, 2, 1),
            EndDate = new DateTime(2026, 5, 31),
            IsCurrent = false,
            Description = "Worked within a real-world development environment supporting a live ASP.NET MVC application.",
            TechEnvironment = "C# · ASP.NET MVC · HTML5 · CSS3 · JavaScript · Bootstrap · SQL Server · Git · GitHub",
            DisplayOrder = 1,
            ProfileId = profileId,
            CreatedAt = dateTimeService.UtcNow
        };

        await context.Experiences.AddAsync(kge);
        await context.SaveChangesAsync();

        var kgeResponsibilities = new List<string>
        {
            "Contributed to development and improvement of a live ASP.NET MVC web application.",
            "Investigated and resolved QA-reported bugs and application issues.",
            "Debugged frontend and backend issues to identify root causes and implement fixes.",
            "Worked on navigation and content-consistency improvements.",
            "Implemented and refined form validations and user-input handling.",
            "Contributed to UI/UX improvements using HTML, CSS, JavaScript, and Bootstrap.",
            "Worked with existing project codebases and repositories using Git and GitHub.",
            "Gained practical exposure to real-world development, debugging, QA, and issue-resolution workflows."
        };

        var order = 0;
        foreach (var r in kgeResponsibilities)
        {
            await context.ExperienceResponsibilities.AddAsync(new ExperienceResponsibility
            {
                Id = Guid.NewGuid(),
                Description = r,
                DisplayOrder = ++order,
                ExperienceId = kge.Id,
                CreatedAt = dateTimeService.UtcNow
            });
        }

        // 2. Naresh IT Training
        var naresh = new Experience
        {
            Id = Guid.NewGuid(),
            Company = "Naresh IT",
            Role = "Full Stack .NET Developer Trainee",
            Location = "Hyderabad",
            StartDate = new DateTime(2025, 5, 1),
            EndDate = new DateTime(2025, 11, 30),
            IsCurrent = false,
            Description = "Completed practical training focused on full-stack web application development.",
            TechEnvironment = "C# · .NET Core · ASP.NET Core · ASP.NET MVC · Web API · Entity Framework · ADO.NET · HTML5 · CSS3 · JavaScript · Bootstrap · SQL Server · Git · GitHub · Agile/Scrum",
            DisplayOrder = 2,
            ProfileId = profileId,
            CreatedAt = dateTimeService.UtcNow
        };

        await context.Experiences.AddAsync(naresh);
        await context.SaveChangesAsync();

        var nareshResponsibilities = new List<string>
        {
            "Completed practical training on full-stack web application development.",
            "Built backend functionality using C#, .NET Core, ASP.NET Core, Web API, and Entity Framework.",
            "Developed responsive interfaces using HTML5, CSS3, JavaScript, and Bootstrap.",
            "Worked with Microsoft SQL Server — complex queries, joins, stored procedures, and optimization.",
            "Practiced MVC Architecture, CRUD operations, debugging, and Git/GitHub workflows.",
            "Applied Agile/Scrum methodologies in team-based projects."
        };

        order = 0;
        foreach (var r in nareshResponsibilities)
        {
            await context.ExperienceResponsibilities.AddAsync(new ExperienceResponsibility
            {
                Id = Guid.NewGuid(),
                Description = r,
                DisplayOrder = ++order,
                ExperienceId = naresh.Id,
                CreatedAt = dateTimeService.UtcNow
            });
        }

        await context.SaveChangesAsync();
    }
}