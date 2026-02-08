using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Entities;

[Table("users")]
public class User
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }
    [Column("email")]
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string? Email { get; set; }
    [Column("password_hash")]
    [Required(ErrorMessage = "Password is required.")]
    public string? PasswordHash { get; set; }
    [Required(ErrorMessage = "Role is required.")]
    [Column("role")]
    public string? Role { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}