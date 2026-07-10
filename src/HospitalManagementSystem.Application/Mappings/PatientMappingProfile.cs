using AutoMapper;
using HospitalManagementSystem.Application.DTOs;
using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Application.Mappings;

public class PatientMappingProfile : Profile
{
    public PatientMappingProfile()
    {
        CreateMap<Patient, PatientDto>();
        CreateMap<CreatePatientDto, Patient>();
        CreateMap<UpdatePatientDto, Patient>();
    }
}
