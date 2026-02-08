namespace Capstone.DTOs;

public class EnrollementDto
{

    public int Id { get; set; }

    public int UserId { get; set; }
    public int PolicyId { get; set; }

    public string? Status { get; set; }

    public DateTime RequestedAt { get; set; }

    public DateTime? ApprovedAt { get; set; }
}