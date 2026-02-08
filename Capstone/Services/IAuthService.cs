namespace Capstone.Services;

using Capstone.DTOs;
using Capstone.Entities;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    string GenerateJwtToken(User user);

}