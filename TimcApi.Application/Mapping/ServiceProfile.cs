using AutoMapper;
using TimcApi.Application.DTOs;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Mapping
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            // Domain → DTO
            CreateMap<Service, ServiceDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            // DTO → Domain
            CreateMap<CreateServiceDto, Service>();
            CreateMap<UpdateServiceDto, Service>();

        }
    }

}
