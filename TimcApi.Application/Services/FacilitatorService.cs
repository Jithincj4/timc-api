using AutoMapper;
using TimcApi.Application.DTOs;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Services
{
    public class FacilitatorService : IFacilitatorService
    {
        private readonly IFacilitatorRepository _repository;
        private readonly IMapper _mapper;

        public FacilitatorService(IFacilitatorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FacilitatorDto>> GetAllAsync()
        {
            var facilitators = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<FacilitatorDto>>(facilitators);
        }

        public async Task<FacilitatorDto?> GetByIdAsync(int id)
        {
            var facilitator = await _repository.GetByIdAsync(id);
            return facilitator != null ? _mapper.Map<FacilitatorDto>(facilitator) : null;
        }

        public async Task<int> CreateAsync(CreateUserAndFacilitator dto)
        {
            // Validate input
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // Map DTO to entity
            var facilitator = _mapper.Map<Facilitator>(dto.FacilitatorDto);
            var user = _mapper.Map<User>(dto.UserDto);

            // Set creation timestamp
            facilitator.CreatedAt = DateTime.UtcNow;

            // Create in repository
            int newId = await _repository.CreateAsync(facilitator,user);

            return newId;
        }

        public async Task UpdateAsync(FacilitatorDto dto)
        {
            // Validate input
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // Get existing entity
            var existingFacilitator = await _repository.GetByIdAsync(dto.FacilitatorId);
            if (existingFacilitator == null)
                throw new KeyNotFoundException($"Facilitator with ID {dto.FacilitatorId} not found");

            // Preserve original creation timestamp
            var originalCreatedAt = existingFacilitator.CreatedAt;

            // Map DTO to existing entity
            _mapper.Map(dto, existingFacilitator);

            // Restore original creation timestamp
            existingFacilitator.CreatedAt = originalCreatedAt;

            // Update in repository
            await _repository.UpdateAsync(existingFacilitator);
        }

        public async Task DeleteAsync(int id)
        {
            // Validate existence
            var existingFacilitator = await _repository.GetByIdAsync(id);
            if (existingFacilitator == null)
                throw new KeyNotFoundException($"Facilitator with ID {id} not found");

            // Delete from repository
            await _repository.DeleteAsync(id);
        }
    }

}
