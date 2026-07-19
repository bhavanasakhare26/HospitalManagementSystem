using AutoMapper;
using HospitalManagementSystem.Application.DTOs;
using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Api.Controllers;
[Authorize]
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null) return NotFound();
        var appointmentDto = _mapper.Map<AppointmentDto>(appointment);
        return Ok(appointmentDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? patientId, [FromQuery] int? doctorId, [FromQuery] int page = 1,[FromQuery] int pageSize = 10, [FromQuery] string? sortBy = null, [FromQuery] bool sortDescending = false)
    {
        var appointments = await _appointmentRepository.GetFilteredAsync(patientId, doctorId, page, pageSize, sortBy, sortDescending);
        if(appointments == null || appointments.Count() == 0) return NotFound();
        var appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointments);
        return Ok(appointmentDtos);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentDto createAppointmentDto)
    {
        if(createAppointmentDto == null) return BadRequest();
        var appointment = _mapper.Map<Appointment>(createAppointmentDto);
        appointment.CreatedAt = DateTime.UtcNow;
        await _appointmentRepository.AddAsync(appointment);
        var newAppointment = await _appointmentRepository.GetByIdAsync(appointment.Id);
        var appointmentDto = _mapper.Map<AppointmentDto>(newAppointment);
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
