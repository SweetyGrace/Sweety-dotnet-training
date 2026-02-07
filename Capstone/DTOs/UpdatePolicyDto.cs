using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.DTOs
{
    public class UpdatePolicyDto
    {
       [StringLength(100, ErrorMessage = "Policy name cannot exceed 100 characters.")]
       [MinLength(3, ErrorMessage = "Policy name must be at least 3 characters long.")]
       [Column("policy_name")]
        public string? PolicyName { get; set; }

        [StringLength(500, ErrorMessage = "Policy description cannot exceed 500 characters.")]
        [MinLength(10, ErrorMessage = "Policy description must be at least 10 characters long.")]
        [Column("policy_description")]
        public string? PolicyDescription { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Premium amount must be greater than zero.")]
        [Column("premium_amount")]
        public decimal? PremiumAmount { get; set; }
        
        [Column("is_active")]
        public bool? IsActive { get; set; }
    }
}