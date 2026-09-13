using FluentValidation;
using SubhashPortfolio.Application.Features.Skills.DTOs;

namespace SubhashPortfolio.Application.Features.Skills.Validators;

public class UpdateSkillDtoValidator : AbstractValidator<UpdateSkillDto>
{
    public UpdateSkillDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Category).IsInEnum();
        RuleFor(x => x.Proficiency).IsInEnum();
        RuleFor(x => x.YearsOfExperience).GreaterThanOrEqualTo(0).When(x => x.YearsOfExperience.HasValue);
    }
}