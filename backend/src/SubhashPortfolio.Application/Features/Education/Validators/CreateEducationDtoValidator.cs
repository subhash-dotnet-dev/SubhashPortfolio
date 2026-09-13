using FluentValidation;
using SubhashPortfolio.Application.Features.Education.DTOs;

namespace SubhashPortfolio.Application.Features.Education.Validators;

public class CreateEducationDtoValidator : AbstractValidator<CreateEducationDto>
{
    public CreateEducationDtoValidator()
    {
        RuleFor(x => x.Degree).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Institution).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Level).IsInEnum();
        RuleFor(x => x.StartYear).InclusiveBetween(1900, 2100).When(x => x.StartYear.HasValue);
        RuleFor(x => x.CompletionYear).InclusiveBetween(1900, 2100);
        RuleFor(x => x.ProfileId).NotEmpty();
    }
}