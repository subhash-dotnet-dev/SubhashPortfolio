using FluentValidation;
using SubhashPortfolio.Application.Features.Contact.DTOs;

namespace SubhashPortfolio.Application.Features.Contact.Validators;

public class UpdateContactMessageDtoValidator : AbstractValidator<UpdateContactMessageDto>
{
    public UpdateContactMessageDtoValidator()
    {
        RuleFor(x => x.Status).IsInEnum().WithMessage("Invalid contact status.");
    }
}