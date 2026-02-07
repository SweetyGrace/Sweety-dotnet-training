using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Capstone.Entities;

[Table("policy")]
public class Policy
{
    [Column("id")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Policy name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Policy name must be between 3 and 100 characters.")]
    [Column("policy_name")]
    public string? PolicyName { get; set; }

    [Required(ErrorMessage = "Policy description is required.")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Policy description must be between 10 and 500 characters.")]
    [Column("policy_description")]
    public string? PolicyDescription { get; set; }
   
    [Required(ErrorMessage = "Premium amount is required.")]
    [Range(1, 1000000, ErrorMessage = "Premium amount must be greater than zero.")]
    [Column("premium_amount")]
    public int PremiumAmount { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}