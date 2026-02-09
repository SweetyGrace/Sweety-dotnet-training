namespace Capstone.Services;
using Capstone.Entities;
using Capstone.DTOs;
using Capstone.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IUserRepository userRepository, IConfiguration configuration, ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        _logger.LogInformation("Registration attempt for email: {Email}", registerDto.Email);
        
        // Check if user with the same email already exists
        var existingUser = await _userRepository.GetUserByEmailAsync(registerDto.Email);
        if (existingUser != null)
        {
            _logger.LogWarning("Registration failed - Email already exists: {Email}", registerDto.Email);
            throw new Exception("Email is already registered.");
        }

        // Create new user
        var newUser = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.PasswordHash),
            Role = registerDto.Role,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var createdUser = await _userRepository.AddUserAsync(newUser);
        var token = GenerateJwtToken(createdUser);
        
        _logger.LogInformation("User registered successfully - ID: {UserId}, Email: {Email}", createdUser.Id, createdUser.Email);

        return new AuthResponseDto
        {
            User = new UserDto
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Email = createdUser.Email,
                Role = createdUser.Role
            },
            Token = token
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        _logger.LogInformation("Login attempt for email: {Email}", loginDto.Email);
        
        var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.PasswordHash, user.PasswordHash))
        {
            _logger.LogWarning("Login failed for email: {Email}", loginDto.Email);
            throw new Exception("Invalid email or password.");
        }

        var token = GenerateJwtToken(user);
        
        _logger.LogInformation("User logged in successfully - ID: {UserId}, Email: {Email}, Role: {Role}", user.Id, user.Email, user.Role);

        return new AuthResponseDto
        {
            User = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            },
            Token = token
        };
    }

    public string GenerateJwtToken(User user)
    {
       //CreatING token handler
        var tokenHandler = new JwtSecurityTokenHandler();
        
        // GetTING secret key from configuration
        var secret = _configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT Secret is not configured.");
        var key = Encoding.UTF8.GetBytes(secret);
        
        //  Defining claims (user information in the token)
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.Name, user.Name ?? string.Empty),
            new Claim(ClaimTypes.Role, user.Role ?? string.Empty)
        };
        
       //Creating token descriptor with all settings
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(24), // Token valid for 24 hours
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        
        //Creating the token
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        //  Converting token to string and return
        return tokenHandler.WriteToken(token);
    }
}