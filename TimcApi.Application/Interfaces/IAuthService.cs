using TimcApi.Application.DTOs;

namespace TimcApi.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(UserLoginDto dto);
        Task<bool> RegisterAsync(UserRegisterDto dto);
    }
}
