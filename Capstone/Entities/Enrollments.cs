using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Entities;

[Table("enrollments")]
public class Enrollment
{
 
 [Column("id")]
 public int Id { get; set; }

[Required(ErrorMessage = "UserId is required.")]
[Column("user_id")]
 public int UserId { get; set; }
[Column("policy_id")]
[Required(ErrorMessage = "PolicyId is required.")]
 public int PolicyId { get; set; }

[Required(ErrorMessage = "Status is required.")]
[Column("status")]
public string Status { get; set; } = "Pending"; 

[Column("requested_at")]
 public DateTime RequestedAt { get; set; }
[Column("approved_at")]
 public DateTime? ApprovedAt { get; set; }

}