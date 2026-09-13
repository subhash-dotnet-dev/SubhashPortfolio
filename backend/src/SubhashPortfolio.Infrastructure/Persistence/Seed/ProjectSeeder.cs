using SubhashPortfolio.Application.Common.Interfaces;
using SubhashPortfolio.Domain.Entities;
using SubhashPortfolio.Domain.Enums;
using SubhashPortfolio.Infrastructure.Persistence.Context;

namespace SubhashPortfolio.Infrastructure.Persistence.Seed;

public static class ProjectSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        Guid profileId,
        IDateTimeService dateTimeService)
    {
        if (context.Projects.Any()) return;

        var projects = new List<(Project Project, List<string> Features, List<string> Techs)>
        {
            (
                new Project
                {
                    Id = Guid.NewGuid(),
                    Title = "AI Resume Analyzer & Smart Job Matcher",
                    Subtitle = "AI × Career Intelligence",
                    ShortDescription = "A full-stack application designed to help job seekers analyze resumes, identify relevant skills, compare resumes against job requirements, and receive actionable improvement suggestions.",
                    Problem = "Job seekers struggle to understand how well their resume matches a job description.",
                    Solution = "An AI-powered tool that analyzes resume content, extracts skills, and matches them against job descriptions with a match score and improvement suggestions.",
                    Architecture = "React.js frontend → ASP.NET Core Web API → AI API integration → EF Core → SQL Server.",
                    EngineeringChallenges = "Resume text extraction from PDFs, integrating AI service, and computing meaningful match scores.",
                    Results = "Users receive a match percentage, missing skill list, and personalized improvement suggestions.",
                    Status = ProjectStatus.Published,
                    DisplayOrder = 1,
                    IsFeatured = true,
                    ProfileId = profileId,
                    CreatedAt = dateTimeService.UtcNow
                },
                new List<string>
                {
                    "Resume upload & text extraction",
                    "Skill identification",
                    "Job-description analysis",
                    "Resume-to-job matching with score",
                    "Missing-skill detection",
                    "Resume improvement suggestions",
                    "User authentication",
                    "Personalized dashboard",
                    "Responsive interface"
                },
                new List<string>
                {
                    "React.js", "ASP.NET Core Web API", "C#", "Entity Framework Core", "SQL Server", "AI API"
                }
            ),
            (
                new Project
                {
                    Id = Guid.NewGuid(),
                    Title = "E-Commerce Web Application",
                    Subtitle = "Commerce × Full-Stack Engineering",
                    ShortDescription = "A modern e-commerce application demonstrating realistic customer, product, cart, order, inventory, and checkout workflows.",
                    Problem = "Building a realistic e-commerce flow that covers catalog, cart, orders, and admin operations.",
                    Solution = "A full-stack app with customer and admin workflows, JWT auth, and transactional order handling.",
                    Architecture = "React.js → ASP.NET Core Web API → EF Core → SQL Server with JWT auth.",
                    EngineeringChallenges = "Handling inventory updates, transactional orders, and admin workflows safely.",
                    Results = "Complete e-commerce flow with authentication, catalog, cart, orders, and admin panel.",
                    Status = ProjectStatus.Published,
                    DisplayOrder = 2,
                    IsFeatured = true,
                    ProfileId = profileId,
                    CreatedAt = dateTimeService.UtcNow
                },
                new List<string>
                {
                    "Product catalog & search",
                    "Advanced filtering",
                    "Product details page",
                    "Shopping cart & wishlist",
                    "Checkout workflow",
                    "Orders & reviews",
                    "Inventory management",
                    "Customer authentication",
                    "Admin workflows",
                    "Pagination & validation"
                },
                new List<string>
                {
                    "React.js", "ASP.NET Core Web API", "C#", "Entity Framework Core", "SQL Server", "JWT"
                }
            ),
            (
                new Project
                {
                    Id = Guid.NewGuid(),
                    Title = "Real-Time Chat & Collaboration App",
                    Subtitle = "Real-Time × Communication",
                    ShortDescription = "A real-time communication application built around instant messaging and live user interaction.",
                    Problem = "Users need a real-time messaging platform with presence, typing indicators, and notifications.",
                    Solution = "SignalR-based real-time chat with groups, presence, typing indicators, and message history.",
                    Architecture = "React.js → ASP.NET Core + SignalR → EF Core → SQL Server.",
                    EngineeringChallenges = "Real-time event handling, presence tracking, and scalable message delivery.",
                    Results = "Working chat app with one-to-one messaging, group chats, and live indicators.",
                    Status = ProjectStatus.Published,
                    DisplayOrder = 3,
                    IsFeatured = true,
                    ProfileId = profileId,
                    CreatedAt = dateTimeService.UtcNow
                },
                new List<string>
                {
                    "One-to-one messaging",
                    "Group conversations",
                    "Online/offline presence",
                    "Typing indicators",
                    "Message read status",
                    "Notifications",
                    "Message history",
                    "File sharing",
                    "Message search",
                    "Authentication"
                },
                new List<string>
                {
                    "React.js", "ASP.NET Core", "SignalR", "C#", "Entity Framework Core", "SQL Server"
                }
            ),
            (
                new Project
                {
                    Id = Guid.NewGuid(),
                    Title = "Event Ticket Booking & Seat Reservation",
                    Subtitle = "Booking × Transactions × Concurrency",
                    ShortDescription = "A booking application designed around event discovery, interactive seat selection, temporary reservations, and reliable ticket-booking workflows.",
                    Problem = "Concurrent seat booking requires transaction-aware logic to avoid double bookings.",
                    Solution = "Transaction-aware booking with temporary seat reservation and interactive seat map.",
                    Architecture = "React.js → ASP.NET Core Web API → EF Core (transactions) → SQL Server.",
                    EngineeringChallenges = "Concurrency control, transaction handling, and temporary seat reservations.",
                    Results = "Reliable booking flow with seat selection, e-ticket generation, and cancellation.",
                    Status = ProjectStatus.Published,
                    DisplayOrder = 4,
                    IsFeatured = true,
                    ProfileId = profileId,
                    CreatedAt = dateTimeService.UtcNow
                },
                new List<string>
                {
                    "Event discovery & details",
                    "Interactive seat map",
                    "Seat selection",
                    "Temporary seat reservation",
                    "Booking confirmation",
                    "Booking history",
                    "Cancellation",
                    "E-ticket generation",
                    "Transaction-aware booking"
                },
                new List<string>
                {
                    "React.js", "ASP.NET Core Web API", "C#", "Entity Framework Core", "SQL Server", "JWT"
                }
            ),
            (
                new Project
                {
                    Id = Guid.NewGuid(),
                    Title = "Personal Finance & Expense Intelligence",
                    Subtitle = "Finance × Data Analytics",
                    ShortDescription = "A personal finance application designed to help users track income and expenses while understanding spending patterns through dashboards and analytics.",
                    Problem = "Users need better visibility into their spending patterns and budgets.",
                    Solution = "A finance tracker with budgets, recurring transactions, savings goals, and analytics dashboards.",
                    Architecture = "React.js → ASP.NET Core Web API → EF Core → SQL Server with SQL aggregation for analytics.",
                    EngineeringChallenges = "Designing SQL aggregation queries and meaningful financial analytics.",
                    Results = "Clear financial dashboard with spending analytics, budgets, and CSV export.",
                    Status = ProjectStatus.Published,
                    DisplayOrder = 5,
                    IsFeatured = true,
                    ProfileId = profileId,
                    CreatedAt = dateTimeService.UtcNow
                },
                new List<string>
                {
                    "Income tracking",
                    "Expense tracking",
                    "Categories",
                    "Monthly budgets",
                    "Recurring transactions",
                    "Savings goals",
                    "Spending analytics",
                    "Financial dashboard",
                    "Search & filtering",
                    "Reports & CSV export"
                },
                new List<string>
                {
                    "React.js", "ASP.NET Core Web API", "C#", "Entity Framework Core", "SQL Server"
                }
            )
        };

        foreach (var (project, features, techs) in projects)
        {
            await context.Projects.AddAsync(project);
            await context.SaveChangesAsync();

            var fOrder = 0;
            foreach (var f in features)
            {
                await context.ProjectFeatures.AddAsync(new ProjectFeature
                {
                    Id = Guid.NewGuid(),
                    Description = f,
                    DisplayOrder = ++fOrder,
                    ProjectId = project.Id,
                    CreatedAt = dateTimeService.UtcNow
                });
            }

            var tOrder = 0;
            foreach (var t in techs)
            {
                await context.ProjectTechs.AddAsync(new ProjectTech
                {
                    Id = Guid.NewGuid(),
                    Name = t,
                    DisplayOrder = ++tOrder,
                    ProjectId = project.Id,
                    CreatedAt = dateTimeService.UtcNow
                });
            }

            await context.SaveChangesAsync();
        }
    }
}