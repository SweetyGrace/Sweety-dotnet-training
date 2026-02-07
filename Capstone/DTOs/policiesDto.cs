using System.ComponentModel.DataAnnotations.Schema;
namespace Capstone.DTOs;

public class PolicyDto
{
    public int Id { get; set; }
    [Column("policy_name")]
    public string? PolicyName { get; set; }

    public string? PolicyDescription { get; set; }
   
    public int PremiumAmount { get; set; }

    public bool IsActive { get; set; }

    
}