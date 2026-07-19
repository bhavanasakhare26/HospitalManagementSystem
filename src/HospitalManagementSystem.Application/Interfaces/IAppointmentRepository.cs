using HospitalManagementSystem.Domain.Entities;
namespace HospitalManagementSystem.Application.Interfaces;

public interface IAppointmentRepository
{
    Task<Appointment> GetByIdAsync(int id);
    Task AddAsync(Appointment appointment);
    Task UpdateAsync(Appointment appointment);
    Task DeleteAsync(Appointment appointment);
    Task<IEnumerable<Appointment>> GetFilteredAsync(int? patientId, int? doctorId, int page, int pageSize, string? sortBy, bool sortDescending = false);
}
