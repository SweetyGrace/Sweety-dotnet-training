namespace Capstone.Controller;
using Capstone.Entities;
using Capstone.Repositories;
using Capstone.Services;
using Capstone.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api")]
[Authorize]
public class EnrollmentController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;
    public EnrollmentController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpPost("policies/{policyId}/enroll", Name = "EnrollUserInPolicy")]
    public async Task<IActionResult> EnrollUserInPolicy(int policyId)
    {
       var UserIdFromToken = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? "0");
       //        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (UserIdFromToken == 0)
        {
            return Unauthorized(new { message = "Invalid token. User ID not found." });
        }
        var enrollment = await _enrollmentService.AddEnrollmentAsync(new Enrollment { UserId = UserIdFromToken, PolicyId = policyId, Status = "Active" });
        return Ok(enrollment);
      
    }

    [HttpGet("my/enrollments", Name = "GetMyEnrollments")]
    public async Task<IActionResult> GetMyEnrollments()
    {
        var UserIdFromToken = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? "0");
        if (UserIdFromToken == 0)
        {
            return Unauthorized(new { message = "Invalid token. User ID not found." });
        }
        var enrollments = await _enrollmentService.GetEnrollmentsByUserIdAsync(UserIdFromToken);
        return Ok(enrollments);
    }
}
