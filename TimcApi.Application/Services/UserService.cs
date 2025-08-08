using AutoMapper;
using TimcApi.Application.DTOs;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<int> CreateAsync(CreateUserDto dto)
        {
            var entity = _mapper.Map<User>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.IsActive = true;

            return await _repo.CreateAsync(entity);
        }

        public async Task UpdateAsync(UserDto dto)
        {
            var entity = _mapper.Map<User>(dto);
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var user = await _repo.GetByEmailAsync(email);
            return _mapper.Map<UserDto>(user);
        }
    }

}
