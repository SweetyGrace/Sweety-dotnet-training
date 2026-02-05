using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearningWebApi.Models.Entities;

[Table("customer")]
public class Customer
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    [Column("email")]
    public string Email { get; set; } = string.Empty;
    [Column("phone")]
    public string Phone { get; set; } = string.Empty;
}