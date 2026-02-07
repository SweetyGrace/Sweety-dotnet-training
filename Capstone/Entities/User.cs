using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Entities;

[Table("users")]
public class User
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string? Name { get; set; }
    [Column("email")]
    public string? Email { get; set; }
    [Column("password_hash")]
    public string? PasswordHash { get; set; }
    [Column("role")]
    public string? Role { get; set; }
}