using FluentValidation;
using FluentValidation.AspNetCore;
using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Application.Mappings;
using HospitalManagementSystem.Application.Validators;
using HospitalManagementSystem.Infrastructure.Data;
using HospitalManagementSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<PatientMappingProfile>();
    cfg.AddProfile<DoctorMappingProfile>();
    cfg.AddProfile<AppointmentMappingProfile>();
});

//Keep only this one — it finds ALL validators in the Application assembly
builder.Services.AddValidatorsFromAssemblyContaining<CreatePatientDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
