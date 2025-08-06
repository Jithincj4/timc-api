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
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repo;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceDto>> GetAllAsync()
        {
            var result = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceDto>>(result);
        }

        public async Task<int> CreateAsync(CreateServiceDto dto)
        {
            var entity = _mapper.Map<Service>(dto);
            return await _repo.CreateAsync(entity);
        }

        public async Task UpdateAsync(ServiceDto dto)
        {
            var entity = _mapper.Map<Service>(dto);
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
