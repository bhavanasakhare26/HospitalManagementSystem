using FluentValidation;
using HospitalManagementSystem.Application.DTOs;

namespace HospitalManagementSystem.Application.Validators;

public class UpdateAppointmentDtoValidator : AbstractValidator<UpdateAppointmentDto>
{
    public UpdateAppointmentDtoValidator()
    {
        RuleFor(x => x.PatientId).GreaterThan(0);
        RuleFor(x => x.DoctorId).GreaterThan(0);
        RuleFor(x => x.AppointmentDate).NotEmpty().GreaterThan(DateTime.Today);
        RuleFor(x => x.Reason).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Status).NotEmpty().MaximumLength(30);
    }
}
