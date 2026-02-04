using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearningWebApi.Models.Entities;

[Table("customer")]
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}