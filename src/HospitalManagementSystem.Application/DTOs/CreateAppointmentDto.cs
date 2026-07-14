namespace HospitalManagementSystem.Application.DTOs;

public class CreateAppointmentDto
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public required string Reason { get; set; }
    public required string Status { get; set; }
}
