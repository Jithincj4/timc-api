using AutoMapper;
using TimcApi.Application.DTOs;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Mapping
{
    public class SaccoProfile : Profile
    {
        public SaccoProfile()
        {
            CreateMap<SACCO, SaccoDto>().ReverseMap();
            CreateMap<CreateSaccoDto, SACCO>();
        }
    }
}
