using FluentValidation;
using HospitalManagementSystem.Application.DTOs;

namespace HospitalManagementSystem.Application.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
