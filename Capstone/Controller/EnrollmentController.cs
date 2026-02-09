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
    private readonly ILogger<EnrollmentController> _logger;
    
    public EnrollmentController(IEnrollmentService enrollmentService, ILogger<EnrollmentController> logger)
    {
        _enrollmentService = enrollmentService;
        _logger = logger;
    }

    [HttpPost("policies/{policyId}/enroll", Name = "EnrollUserInPolicy")]
    public async Task<IActionResult> EnrollUserInPolicy(int policyId)
    {
       var UserIdFromToken = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? "0");
       //        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (UserIdFromToken == 0)
        {
            _logger.LogWarning("Enrollment attempt with invalid token - PolicyId: {PolicyId}", policyId);
            return Unauthorized(new { message = "Invalid token. User ID not found." });
        }
        
        _logger.LogInformation("Enrollment request - UserId: {UserId}, PolicyId: {PolicyId}", UserIdFromToken, policyId);
        var enrollment = await _enrollmentService.AddEnrollmentAsync(new Enrollment { UserId = UserIdFromToken, PolicyId = policyId, Status = "Active" });
        return Ok(enrollment);
      
    }

    [HttpGet("my/enrollments", Name = "GetMyEnrollments")]
    public async Task<IActionResult> GetMyEnrollments()
    {
        var UserIdFromToken = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? "0");
        if (UserIdFromToken == 0)
        {
            _logger.LogWarning("Attempt to get enrollments with invalid token");
            return Unauthorized(new { message = "Invalid token. User ID not found." });
        }
        
        _logger.LogInformation("Fetching enrollments for UserId: {UserId}", UserIdFromToken);
        var enrollments = await _enrollmentService.GetEnrollmentsByUserIdAsync(UserIdFromToken);
        return Ok(enrollments);
    }
}
