using FluentValidation;
using SubhashPortfolio.Application.Features.SocialLinks.DTOs;

namespace SubhashPortfolio.Application.Features.SocialLinks.Validators;

public class UpdateSocialLinkDtoValidator : AbstractValidator<UpdateSocialLinkDto>
{
    public UpdateSocialLinkDtoValidator()
    {
        RuleFor(x => x.Platform).IsInEnum();
        RuleFor(x => x.DisplayName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Url).NotEmpty().MaximumLength(2048).Must(BeAValidUrl).WithMessage("Please provide a valid URL.");
        RuleFor(x => x.IconClass).MaximumLength(100).When(x => !string.IsNullOrEmpty(x.IconClass));
    }

    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}