namespace Capstone.Controller;
using Capstone.Entities;
using Capstone.Repositories;
using Capstone.Services;
using Capstone.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminEnrollmentController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;
    private readonly IPolicyService policyService;
    private readonly ILogger<AdminEnrollmentController> _logger;
    
    public AdminEnrollmentController(IEnrollmentService enrollmentService, IPolicyService _policyService, ILogger<AdminEnrollmentController> logger)
    {
        _enrollmentService = enrollmentService;
        this.policyService = _policyService;
        _logger = logger;
    }

       [HttpPost("policies", Name = "AdminCreatePolicy")]
    public async Task<IActionResult> CreatePolicy([FromBody] Policy policy)
    {
        _logger.LogInformation("Admin creating new policy - Name: {PolicyName}", policy.PolicyName);
        
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Policy creation failed - Model validation errors");
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new 
                {
                    Field = x.Key,
                    Errors = x.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                })
                .ToList();

            return BadRequest(new 
            { 
                message = "Validation failed.", 
                errors = errors 
            });
        }

        var createdPolicy = await policyService.CreatePolicyAsync(policy);
        _logger.LogInformation("Policy created successfully - PolicyId: {PolicyId}", createdPolicy.Id);
        return CreatedAtRoute("GetPolicyById", new { id = createdPolicy.Id }, createdPolicy);
    }

       [HttpPut("policies/{id}", Name = "AdminUpdatePolicy")]
    public async Task<IActionResult> UpdatePolicy(int id, [FromBody] UpdatePolicyDto updateDto)
    {
        _logger.LogInformation("Admin updating policy - PolicyId: {PolicyId}", id);
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedPolicy = await policyService.UpdatePolicyAsync(id, updateDto);
        if (updatedPolicy == null)
        {
            return NotFound(new { message = $"Policy with ID {id} not found." });
        }

        return Ok(updatedPolicy);
    }

    [HttpGet("enrollments", Name = "GetAllEnrollments")]
    public async Task<IActionResult> GetAllEnrollments()
    {
        var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
        return Ok(enrollments);
    }

    [HttpGet("enrollments/status", Name = "GetEnrollmentsByStatus")]
    public async Task<IActionResult> GetEnrollmentsByStatus(string status)
    {
        var enrollments = await _enrollmentService.GetEnrollmentsByStatusAsync(status);
        return Ok(enrollments);
    }

    [HttpPost("enrollments/{id}/approve", Name = "ApproveEnrollment")]
    public async Task<IActionResult> ApproveEnrollment(int id)
    {
        _logger.LogInformation("Admin attempting to approve enrollment - EnrollmentId: {EnrollmentId}", id);
        var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
        if (enrollment == null)
        {
            return NotFound(new { message = $"Enrollment with ID {id} not found." });
        }
        var approvedEnrollment = await _enrollmentService.ApproveEnrollmentAsync(id);
        return Ok(approvedEnrollment);
    }

    [HttpPost("enrollments/{id}/reject", Name = "RejectEnrollment")]
    public async Task<IActionResult> RejectEnrollment(int id)
    {
        _logger.LogInformation("Admin attempting to reject enrollment - EnrollmentId: {EnrollmentId}", id);
        var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
        if (enrollment == null)
        {            return NotFound(new { message = $"Enrollment with ID {id} not found." });  
        }
        var rejectedEnrollment = await _enrollmentService.RejectEnrollmentAsync(id);
        return Ok(rejectedEnrollment);
    }
}
