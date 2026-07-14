namespace HospitalManagementSystem.Application.DTOs;

public class AppointmentDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public required string PatientName { get; set; }
    public int DoctorId { get; set; }
    public required string DoctorName { get; set; }
    public DateTime AppointmentDate { get; set; }
    public required string Reason { get; set; }
    public required string Status { get; set; }
    public DateTime CreatedAt { get; set; } 
}
