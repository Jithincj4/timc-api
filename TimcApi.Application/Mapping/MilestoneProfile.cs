using AutoMapper;
using TimcApi.Application.DTOs;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Mapping
{
    public class MilestoneProfile : Profile
    {
        public MilestoneProfile()
        {
            CreateMap<Milestone, MilestoneDto>().ReverseMap();
            CreateMap<CreateMilestoneDto, Milestone>();
        }
    }

}
