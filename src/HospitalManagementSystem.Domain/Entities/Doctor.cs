namespace HospitalManagementSystem.Domain.Entities;

public class Doctor
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Specialization { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
