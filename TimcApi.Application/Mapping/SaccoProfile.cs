using AutoMapper;
using TimcApi.Application.DTOs;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Mapping
{
    public class SaccoProfile : Profile
    {
        public SaccoProfile()
        {
            // Entity ↔ DTO
            CreateMap<SACCO, SaccoDto>().ReverseMap();

            // Create
            CreateMap<CreateSaccoDto, SACCO>();

            // Update
            CreateMap<UpdateSaccoDto, SACCO>()
                .ForMember(dest => dest.AgentId, opt => opt.Ignore()) // Usually we don't overwrite primary key
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Prevent overwriting creation date
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()); // Prevent overwriting creator
        }
    }
}
