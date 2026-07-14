using AutoMapper;
using HospitalManagementSystem.Application.DTOs;
using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IMapper _mapper;

    public AppointmentsController(IAppointmentRepository appointmentRepository, IMapper mapper)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var appointments = await _appointmentRepository.GetAllAsync();
        if(appointments == null || appointments.Count() == 0) return NotFound();
        var appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointments);
        return Ok(appointmentDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null) return NotFound();
        var appointmentDto = _mapper.Map<AppointmentDto>(appointment);
        return Ok(appointmentDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentDto createAppointmentDto)
    {
        if(createAppointmentDto == null) return BadRequest();
        var appointment = _mapper.Map<Appointment>(createAppointmentDto);
        appointment.CreatedAt = DateTime.UtcNow;
        await _appointmentRepository.AddAsync(appointment);
        var appointmentDto = _mapper.Map<AppointmentDto>(appointment);
        return CreatedAtAction(nameof(GetById), new { id = appointmentDto.Id}, appointmentDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAppointmentDto updateAppointmentDto)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null) return NotFound();
        appointment.PatientId = updateAppointmentDto.PatientId;
        appointment.DoctorId = updateAppointmentDto.DoctorId;
        appointment.AppointmentDate = updateAppointmentDto.AppointmentDate;
        appointment.Reason = updateAppointmentDto.Reason;
        appointment.Status = updateAppointmentDto.Status;
        await _appointmentRepository.UpdateAsync(appointment);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null) return NotFound();
        await _appointmentRepository.DeleteAsync(appointment);
        return NoContent();
    }
}
