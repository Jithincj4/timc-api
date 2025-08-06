using AutoMapper;
using TimcApi.Application.DTOs;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Mapping
{
    public class FacilitatorProfile : Profile
    {
        public FacilitatorProfile()
        {
            CreateMap<Facilitator, FacilitatorDto>().ReverseMap();
            CreateMap<CreateFacilitatorDto, Facilitator>();
        }
    }
}
