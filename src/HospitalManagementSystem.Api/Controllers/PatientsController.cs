using Microsoft.AspNetCore.Mvc;
using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientRepository _patientRepository;

    public PatientsController(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var patients = await _patientRepository.GetAllAsync();
        if(patients == null || patients.Count() == 0) return NotFound();
        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if(patient == null) return NotFound();
        return Ok(patient);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Patient patient)
    {
        if(patient == null) return BadRequest();
        patient.CreatedAt = DateTime.UtcNow;
        await _patientRepository.AddAsync(patient);
        return CreatedAtAction(nameof(GetById), new {id = patient.Id}, patient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Patient patient)
    {
        var currentPatient = await _patientRepository.GetByIdAsync(id);
        if(currentPatient == null) return NotFound();

        currentPatient.FirstName = patient.FirstName;
        currentPatient.LastName = patient.LastName;
        currentPatient.DateOfBirth = patient.DateOfBirth;
        currentPatient.Gender = patient.Gender;
        currentPatient.Email = patient.Email;
        currentPatient.PhoneNumber = patient.PhoneNumber;
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
