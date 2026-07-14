using HospitalManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Data.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasOne(a => a.Patient)
        .WithMany(p => p.Appointments).HasForeignKey(a => a.PatientId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Doctor)
        .WithMany(d => d.Appointments).HasForeignKey(a => a.DoctorId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.AppointmentDate).IsRequired();
        builder.Property(p => p.Reason).IsRequired().HasMaxLength(500);
        builder.Property(p => p.Status).IsRequired().HasMaxLength(30);
    }

}
