using AutoMapper;
using HospitalManagementSystem.Application.DTOs;
using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public DoctorsController(IDoctorRepository doctorRepository, IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var doctors = await _doctorRepository.GetAllAsync();
        if(doctors == null || doctors.Count() == 0) return NotFound();
        var doctorDtos = _mapper.Map<List<DoctorDto>>(doctors);
        return Ok(doctorDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var doctor = await _doctorRepository.GetByIdAsync(id);
        if (doctor == null) return NotFound();
        var doctorDto = _mapper.Map<DoctorDto>(doctor);
        return Ok(doctorDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDoctorDto createDoctorDto)
    {
        if(createDoctorDto == null) return BadRequest();
        var doctor = _mapper.Map<Doctor>(createDoctorDto);
        doctor.CreatedAt = DateTime.UtcNow;
        await _doctorRepository.AddAsync(doctor);
        var doctorDto = _mapper.Map<DoctorDto>(doctor);
        return CreatedAtAction(nameof(GetById), new { id = doctorDto.Id}, doctorDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDoctorDto updateDoctorDto)
    {
        var currentDoctor = await _doctorRepository.GetByIdAsync(id);
        if (currentDoctor == null) return NotFound();

        currentDoctor.FirstName = updateDoctorDto.FirstName;
        currentDoctor.LastName = updateDoctorDto.LastName;
        currentDoctor.Email = updateDoctorDto.Email;
        currentDoctor.PhoneNumber = updateDoctorDto.PhoneNumber;
        currentDoctor.Specialization = updateDoctorDto.Specialization;
        await _doctorRepository.UpdateAsync(currentDoctor);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var doctor = await _doctorRepository.GetByIdAsync(id);
        if (doctor == null) return NotFound();
        await _doctorRepository.DeleteAsync(doctor);
        return NoContent();
    }
}
