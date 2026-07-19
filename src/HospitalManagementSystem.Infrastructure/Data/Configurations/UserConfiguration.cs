using HospitalManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.Email).IsRequired().HasMaxLength(50);
        builder.HasIndex(p => p.Email).IsUnique();
        builder.Property(p => p.PasswordHash).IsRequired().HasMaxLength(500);
        builder.Property(p => p.Role).IsRequired().HasMaxLength(20);
    }

}
