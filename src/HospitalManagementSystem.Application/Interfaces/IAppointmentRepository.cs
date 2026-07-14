using HospitalManagementSystem.Domain.Entities;
namespace HospitalManagementSystem.Application.Interfaces;

public interface IAppointmentRepository
{
    Task<Appointment> GetByIdAsync(int id);
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task AddAsync(Appointment appointment);
    Task UpdateAsync(Appointment appointment);
    Task DeleteAsync(Appointment appointment);
}
