using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationDbContext _context;

    public AppointmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Appointment> GetByIdAsync(int id)
    {
        return await _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Appointment appointment)
    {
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Appointment>> GetFilteredAsync(int? patientId, int? doctorId, int page, int pageSize, string? sortBy, bool sortDescending = false)
    {
        var query = _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).AsQueryable();

        if (patientId.HasValue)
            query = query.Where(a => a.PatientId == patientId.Value);
        if (doctorId.HasValue)
            query = query.Where(a => a.DoctorId == doctorId);

        query = sortBy?.ToLower() switch
        {
            "date" => sortDescending ? query.OrderByDescending(a => a.AppointmentDate) : query.OrderBy(a => a.AppointmentDate),
            "status" => sortDescending ? query.OrderByDescending(a => a.Status) : query.OrderBy(a => a.Status),
            _ => query.OrderBy(a => a.AppointmentDate)
        };

        query = query.Skip((page - 1) * pageSize).Take(pageSize);
        return await query.ToListAsync();
    }
}
