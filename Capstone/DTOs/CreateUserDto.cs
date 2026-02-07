using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.DTOs;

public class CreateUserDto
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
    [Column("name")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    [Column("email")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    [Column("password_hash")]
    public string? PasswordHash { get; set; }
    [Required(ErrorMessage = "Role is required.")]
    [Column("role")]
    public string? Role { get; set; }
}