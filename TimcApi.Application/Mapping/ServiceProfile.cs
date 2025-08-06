using AutoMapper;
using TimcApi.Application.DTOs;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Mapping
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<CreateServiceDto, Service>();

            CreateMap<PatientService, PatientServiceDto>().ReverseMap();
            CreateMap<AssignServiceDto, PatientService>();
        }
    }
}
