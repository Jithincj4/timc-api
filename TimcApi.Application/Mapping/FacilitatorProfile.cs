using AutoMapper;
using TimcApi.Application.DTOs;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Mapping
{
    public class FacilitatorProfile : Profile
    {
        public FacilitatorProfile()
        {
            // Entity to DTO
            CreateMap<Facilitator, FacilitatorDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Languages))
                .ForMember(dest => dest.Specializations, opt => opt.MapFrom(src => src.Specializations));

            CreateMap<Language, LanguageDto>();
            CreateMap<Specialization, SpecializationDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.SpecializationName));

            // DTO to Entity
            CreateMap<CreateFacilitatorDto, Facilitator>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.FacilitatorId, opt => opt.Ignore())
                .ForMember(dest => dest.Languages, opt => opt.Ignore())
                .ForMember(dest => dest.Specializations, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.CreatedAt = DateTime.UtcNow;
                    dest.Languages = src.LanguageIds.Select(id => new Language { LanguageId = id }).ToList();
                    dest.Specializations = src.SpecializationIds.Select(id => new Specialization { SpecializationId = id }).ToList();
                });

            CreateMap<UpdateFacilitatorDto, Facilitator>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Languages, opt => opt.Ignore())
                .ForMember(dest => dest.Specializations, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Languages = src.LanguageIds.Select(id => new Language { LanguageId = id }).ToList();
                    dest.Specializations = src.SpecializationIds.Select(id => new Specialization { SpecializationId = id }).ToList();
                });
        }
    }
}
