using FluentValidation;
using SubhashPortfolio.Application.Features.Experience.DTOs;

namespace SubhashPortfolio.Application.Features.Experience.Validators;

public class CreateExperienceDtoValidator : AbstractValidator<CreateExperienceDto>
{
    public CreateExperienceDtoValidator()
    {
        RuleFor(x => x.Company)
            .NotEmpty().WithMessage("Company is required.")
            .MaximumLength(200);

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .MaximumLength(200);

        RuleFor(x => x.Location)
            .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Location));

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate)
            .WithMessage("End date must be after start date.")
            .When(x => x.EndDate.HasValue);

        RuleFor(x => x.Description)
            .MaximumLength(2000).When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.TechEnvironment)
            .MaximumLength(1000).When(x => !string.IsNullOrEmpty(x.TechEnvironment));

        RuleFor(x => x.ProfileId)
            .NotEmpty().WithMessage("ProfileId is required.");

        RuleForEach(x => x.Responsibilities)
            .NotEmpty().MaximumLength(500);
    }
}