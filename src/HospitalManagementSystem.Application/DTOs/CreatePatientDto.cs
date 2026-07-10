namespace HospitalManagementSystem.Application.DTOs;

public class CreatePatientDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public required string Gender { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
}
