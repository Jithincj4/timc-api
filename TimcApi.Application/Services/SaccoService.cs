using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimcApi.Application.DTOs;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Services
{
    public class SaccoService : ISaccoService
    {
        private readonly ISaccoRepository _repo;
        private readonly IMapper _mapper;

        public SaccoService(ISaccoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UpdateSaccoDto>> GetAllAsync()
        {
            var saccos = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<UpdateSaccoDto>>(saccos);
        }

        public async Task<UpdateSaccoDto?> GetByIdAsync(int id)
        {
            var sacco = await _repo.GetByIdAsync(id);
            return _mapper.Map<UpdateSaccoDto>(sacco);
        }

        public async Task<int> CreateAsync(CreateSaccoDto dto, int createdBy)
        {
            var entity = _mapper.Map<SACCO>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = createdBy;
            return await _repo.CreateAsync(entity);
        }

        public async Task UpdateAsync(UpdateSaccoDto dto)
        {
            var entity = _mapper.Map<SACCO>(dto);
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }

}
