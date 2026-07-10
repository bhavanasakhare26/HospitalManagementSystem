using AutoMapper;
using HospitalManagementSystem.Application.DTOs;
using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public PatientsController(IPatientRepository patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var patients = await _patientRepository.GetAllAsync();
        if (patients == null || patients.Count() == 0) return NotFound();
        var patientDtos = _mapper.Map<List<PatientDto>>(patients);
        return Ok(patientDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null) return NotFound();
        var patientDto = _mapper.Map<PatientDto>(patient);
        return Ok(patientDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientDto createPatientDto)
    {
        if(createPatientDto == null) return BadRequest();
        var patient = _mapper.Map<Patient>(createPatientDto);
        patient.CreatedAt = DateTime.UtcNow;
        await _patientRepository.AddAsync(patient);
        var patientDto = _mapper.Map<PatientDto>(patient);
        return CreatedAtAction(nameof(GetById), new {id = patientDto.Id}, patientDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePatientDto updatePatientDto)
    {
        var currentPatient = await _patientRepository.GetByIdAsync(id);
        if(currentPatient == null) return NotFound();

        currentPatient.FirstName = updatePatientDto.FirstName;
        currentPatient.LastName = updatePatientDto.LastName;
        currentPatient.DateOfBirth = updatePatientDto.DateOfBirth;
        currentPatient.Gender = updatePatientDto.Gender;
        currentPatient.Email = updatePatientDto.Email;
        currentPatient.PhoneNumber = updatePatientDto.PhoneNumber;
        await _patientRepository.UpdateAsync(currentPatient);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if(patient == null) return NotFound();
        await _patientRepository.DeleteAsync(patient);
        return NoContent();
    }
}
