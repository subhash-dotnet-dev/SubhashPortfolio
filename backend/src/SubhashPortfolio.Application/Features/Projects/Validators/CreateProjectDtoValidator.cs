using FluentValidation;
using SubhashPortfolio.Application.Features.Projects.DTOs;

namespace SubhashPortfolio.Application.Features.Projects.Validators;

public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200);

        RuleFor(x => x.Subtitle)
            .MaximumLength(300).When(x => !string.IsNullOrEmpty(x.Subtitle));

        RuleFor(x => x.ShortDescription)
            .NotEmpty().WithMessage("Short description is required.")
            .MaximumLength(1000);

        RuleFor(x => x.Problem).MaximumLength(4000).When(x => !string.IsNullOrEmpty(x.Problem));
        RuleFor(x => x.Solution).MaximumLength(4000).When(x => !string.IsNullOrEmpty(x.Solution));
        RuleFor(x => x.Architecture).MaximumLength(4000).When(x => !string.IsNullOrEmpty(x.Architecture));
        RuleFor(x => x.SecurityNotes).MaximumLength(4000).When(x => !string.IsNullOrEmpty(x.SecurityNotes));
        RuleFor(x => x.EngineeringChallenges).MaximumLength(4000).When(x => !string.IsNullOrEmpty(x.EngineeringChallenges));
        RuleFor(x => x.Results).MaximumLength(4000).When(x => !string.IsNullOrEmpty(x.Results));

        RuleFor(x => x.GithubUrl)
            .MaximumLength(2048)
            .Must(BeAValidUrl).WithMessage("Please provide a valid GitHub URL.")
            .When(x => !string.IsNullOrEmpty(x.GithubUrl));

        RuleFor(x => x.LiveDemoUrl)
            .MaximumLength(2048)
            .Must(BeAValidUrl).WithMessage("Please provide a valid Live Demo URL.")
            .When(x => !string.IsNullOrEmpty(x.LiveDemoUrl));

        RuleFor(x => x.ThumbnailUrl)
            .MaximumLength(2048).When(x => !string.IsNullOrEmpty(x.ThumbnailUrl));

        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.ProfileId).NotEmpty();

        RuleForEach(x => x.Features).NotEmpty().MaximumLength(500);
        RuleForEach(x => x.Technologies).NotEmpty().MaximumLength(100);
    }

    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}