namespace HospitalManagementSystem.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string Gender { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}