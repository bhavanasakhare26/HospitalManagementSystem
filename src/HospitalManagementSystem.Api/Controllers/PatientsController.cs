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
public class PatientsController : ControllerBase
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<PatientsController> _logger;

    public PatientsController(IPatientRepository patientRepository, IMapper mapper, ILogger<PatientsController> logger)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Roles="Admin,Receptionist,Doctor")]
    public async Task<IActionResult> GetAll()
    {
        var patients = await _patientRepository.GetAllAsync();
        if (patients == null || patients.Count() == 0)
        {
            _logger.LogInformation("No Patients found");
            return NotFound();
        }
        var patientDtos = _mapper.Map<List<PatientDto>>(patients);
        return Ok(patientDtos);
    }

    [HttpGet("{id}")]
    [Authorize(Roles="Admin,Receptionist,Doctor")]
    public async Task<IActionResult> GetById(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null)
        {
            _logger.LogWarning("Patient with Id {Id} not found", id);
            return NotFound();
        }
            
        var patientDto = _mapper.Map<PatientDto>(patient);
        return Ok(patientDto);
    }

    [HttpPost]
    [Authorize(Roles="Admin,Receptionist")]
    public async Task<IActionResult> Create([FromBody] CreatePatientDto createPatientDto)
    {
        if(createPatientDto == null) return BadRequest();
        var patient = _mapper.Map<Patient>(createPatientDto);
        patient.CreatedAt = DateTime.UtcNow;
        await _patientRepository.AddAsync(patient);
        var patientDto = _mapper.Map<PatientDto>(patient);
        _logger.LogInformation("Created a new Patient with Email : {Email}", createPatientDto.Email);
        return CreatedAtAction(nameof(GetById), new {id = patientDto.Id}, patientDto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles="Admin,Receptionist")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePatientDto updatePatientDto)
    {
        var currentPatient = await _patientRepository.GetByIdAsync(id);
        if (currentPatient == null)
        {
            _logger.LogWarning("Patient with Id {Id} not found for update", id);
            return NotFound();
        }
        currentPatient.FirstName = updatePatientDto.FirstName;
        currentPatient.LastName = updatePatientDto.LastName;
        currentPatient.DateOfBirth = updatePatientDto.DateOfBirth;
        currentPatient.Gender = updatePatientDto.Gender;
        currentPatient.Email = updatePatientDto.Email;
        currentPatient.PhoneNumber = updatePatientDto.PhoneNumber;
        await _patientRepository.UpdateAsync(currentPatient);
        _logger.LogInformation("Updated Patient with Id {Id}", id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if(patient == null) return NotFound();
        await _patientRepository.DeleteAsync(patient);
        _logger.LogInformation("Deleted Patient with Id : {Id}", id);
        return NoContent();
    }
}
