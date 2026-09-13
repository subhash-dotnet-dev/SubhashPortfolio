using FluentValidation;
using SubhashPortfolio.Application.Features.Profile.DTOs;

namespace SubhashPortfolio.Application.Features.Profile.Validators;

public class UpdateProfileDtoValidator : AbstractValidator<UpdateProfileDto>
{
    public UpdateProfileDtoValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Location).NotEmpty().MaximumLength(200);
        RuleFor(x => x.ShortBio).NotEmpty().MaximumLength(500);
        RuleFor(x => x.ProfessionalSummary).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.CareerDirection).NotEmpty().MaximumLength(500);
    }
}