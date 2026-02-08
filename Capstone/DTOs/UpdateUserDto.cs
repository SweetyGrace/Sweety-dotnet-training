using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs;

public class UpdateUserDto
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
    
    public string? Name { get; set; }
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string? Email { get; set; }
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string? PasswordHash { get; set; }
    public string? Role { get; set; }
}