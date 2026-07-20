using HospitalManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAdminAsync(ApplicationDbContext context)
    {
        if (await context.Users.AnyAsync(u => u.Role == "Admin"))
            return;

        var admin = new User
        {
            Email = "admin@hospital.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            Role = "Admin",
            CreatedAt = DateTime.UtcNow
        };

        await context.Users.AddAsync(admin);
        await context.SaveChangesAsync();
    }
}
