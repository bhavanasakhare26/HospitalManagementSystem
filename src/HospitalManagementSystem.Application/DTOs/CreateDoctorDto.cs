namespace HospitalManagementSystem.Application.DTOs;

public class CreateDoctorDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Specialization { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
}
