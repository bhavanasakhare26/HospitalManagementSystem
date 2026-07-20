using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HospitalManagementSystem.Infrastructure.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMemoryCache _cache;
    private const string AllDoctorsCacheKey = "all_doctors";

    public DoctorRepository(ApplicationDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task AddAsync(Doctor doctor)
    {
        await _context.Doctors.AddAsync(doctor);
        await _context.SaveChangesAsync();
        _cache.Remove(AllDoctorsCacheKey);
    }

    public async Task DeleteAsync(Doctor doctor)
    {
        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
        _cache.Remove(AllDoctorsCacheKey);
    }

    public async Task<IEnumerable<Doctor>> GetAllAsync()
    {
        return await _cache.GetOrCreateAsync(AllDoctorsCacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
            return await _context.Doctors.ToListAsync();
        }) ?? new List<Doctor>();
    }

    public async Task<Doctor> GetByIdAsync(int id)
    {
        return await _context.Doctors.FindAsync(id);
    }

    public async Task UpdateAsync(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync();
        _cache.Remove(AllDoctorsCacheKey);
    }
}
