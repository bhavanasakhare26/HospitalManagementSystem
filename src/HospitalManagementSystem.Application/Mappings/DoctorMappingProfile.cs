using AutoMapper;
using HospitalManagementSystem.Application.DTOs;
using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Application.Mappings;

public class DoctorMappingProfile : Profile
{
    public DoctorMappingProfile()
    {
        CreateMap<Doctor, DoctorDto>();
        CreateMap<CreateDoctorDto, Doctor>();
        CreateMap<UpdateDoctorDto, Doctor>();
    }
}
