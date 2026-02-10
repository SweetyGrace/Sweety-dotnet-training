namespace Capstone.Controller;

using System.Runtime.ExceptionServices;
using Microsoft.AspNetCore.Mvc;
using Capstone.Services;
using Capstone.Entities;
using Capstone.DTOs;
using Capstone.Filters;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[ServiceFilter(typeof(GlobalResponseFilter))]
[ServiceFilter(typeof(ResponseTimeFilter))]
[Route("api/policies")]
public class PoliciesController : ControllerBase
{
    private readonly IPolicyService policyService;
    public PoliciesController(IPolicyService _policyService)
    {
        this.policyService = _policyService;
    }

    [HttpGet(Name = "GetPolicies")]
    public async Task<IActionResult> GetPolicies()
    {
     
        return Ok(await policyService.GetAllPoliciesAsync());
    }
    
    [HttpGet("{id}", Name = "GetPolicyById")]
    public async Task<IActionResult> GetPolicyById(int id)
    {
       var policy = await policyService.GetPolicyByIdAsync(id);
         if (policy == null)
            {
                return NotFound(new { message = $"Policy with ID {id} not found." });
            }
            return Ok(policy);
    }

    [HttpGet("search", Name = "SearchPolicies")]
    public async Task<IActionResult> SearchPolicies([FromQuery] int minAmount, [FromQuery] int maxAmount)
    {
        var policies = await policyService.SearchPoliciesAsync(minAmount, maxAmount);
        return Ok(policies);
    }

    [HttpGet("status", Name = "GetPolicyStatus")]
    public async Task<IActionResult> GetPolicyStatus([FromQuery] bool isActive)
    {
        var policies = await policyService.GetPolicyStatus(isActive);
        return Ok(policies);
    }

    [HttpPost(Name = "CreatePolicy")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreatePolicy([FromBody] Policy policy)
    {
        if (!ModelState.IsValid)
        {
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
        return CreatedAtRoute("GetPolicyById", new { id = createdPolicy.Id }, createdPolicy);
    }

    [HttpPut("{id}", Name = "UpdatePolicy")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdatePolicy(int id, [FromBody] UpdatePolicyDto updateDto)
    {
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

    [HttpDelete("{id}", Name = "DeletePolicy")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletePolicy(int id)
    {
        var result = await policyService.DeletePolicyAsync(id);
        if (!result)
        {
            return NotFound(new { message = $"Policy with ID {id} not found." });
        }

        return NoContent();
    }
}
    