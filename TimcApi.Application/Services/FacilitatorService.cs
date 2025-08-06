using AutoMapper;
using TimcApi.Application.DTOs;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Services
{
    public class FacilitatorService : IFacilitatorService
    {
        private readonly IFacilitatorRepository _repo;
        private readonly IMapper _mapper;

        public FacilitatorService(IFacilitatorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FacilitatorDto>> GetAllAsync()
        {
            var data = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<FacilitatorDto>>(data);
        }

        public async Task<FacilitatorDto?> GetByIdAsync(int id)
        {
            var data = await _repo.GetByIdAsync(id);
            return _mapper.Map<FacilitatorDto>(data);
        }

        public async Task<int> CreateAsync(CreateFacilitatorDto dto)
        {
            var facilitator = _mapper.Map<Facilitator>(dto);
            return await _repo.CreateAsync(facilitator);
        }

        public async Task UpdateAsync(FacilitatorDto dto)
        {
            var facilitator = _mapper.Map<Facilitator>(dto);
            await _repo.UpdateAsync(facilitator);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }

}
