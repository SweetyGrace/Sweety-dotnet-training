using Microsoft.AspNetCore.Mvc;
using MyLearningWebApi.Models.DTOs;
using MyLearningWebApi.Services;

namespace MyLearningWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerResponseDto>>> GetAll()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerResponseDto>> GetById(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if(customer == null) return NotFound();
        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerResponseDto>> Create([FromBody] CreateCustomerDto customer)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var createdCustomer = await _customerService.CreateCustomerAsync(customer);
        return CreatedAtAction(nameof(GetById), new { id = createdCustomer.Id }, createdCustomer);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerResponseDto>> Update(int id, [FromBody] UpdateCustomerDto customer)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var updatedCustomer = await _customerService.UpdateCustomerAsync(id, customer);
        if(updatedCustomer == null) return NotFound();
        return Ok(updatedCustomer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _customerService.DeleteCustomerAsync(id);
        if(!deleted) return NotFound();
        return NoContent();
    }

}