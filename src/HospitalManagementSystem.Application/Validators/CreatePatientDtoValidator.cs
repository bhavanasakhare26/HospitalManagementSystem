using FluentValidation;
using HospitalManagementSystem.Application.DTOs;

namespace HospitalManagementSystem.Application.Validators;

public class CreatePatientDtoValidator : AbstractValidator<CreatePatientDto>
{
    public CreatePatientDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(100);
        RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.Today);
        RuleFor(x => x.Gender).NotEmpty().MaximumLength(20);
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(15);
    }
}
