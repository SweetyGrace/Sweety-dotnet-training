using MyLearningWebApi.Models.DTOs;
using MyLearningWebApi.Models.Entities;  
using MyLearningWebApi.Repositories;  

namespace MyLearningWebApi.Services;
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CustomerResponseDto>> GetAllCustomersAsync()
    {
        var customers = await _repository.GetAllAsync();
        return customers.Select(c => new CustomerResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            Phone = c.Phone
        }).ToList();
    }

    public async Task<CustomerResponseDto?> GetCustomerByIdAsync(int id)
    {
        var customer = await _repository.GetByIdAsync(id);
        if(customer == null) return null;
        return new CustomerResponseDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone
        };
    }
    
      public async Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto customerRequest)
    {
        var customer =  new Customer
        {
            Name = customerRequest.Name,
            Email = customerRequest.Email,
            Phone = customerRequest.Phone
        };
        var createdCustomer = await _repository.CreateAsync(customer);
        return new CustomerResponseDto
        {
            Id = createdCustomer.Id,
            Name = createdCustomer.Name,
            Email = createdCustomer.Email,
            Phone = createdCustomer.Phone
        };
    }

     public async Task<CustomerResponseDto> UpdateCustomerAsync(int id,UpdateCustomerDto customerRequest)
    {
        var exists = await _repository.GetByIdAsync(id);
        if(exists == null) return null;
        exists.Name = customerRequest.Name;
        exists.Email = customerRequest.Email;
        exists.Phone = customerRequest.Phone;
        var UpdatedCustomer = await _repository.UpdateAsync(exists);
        return new CustomerResponseDto
        {
            Id = UpdatedCustomer.Id,
            Name = UpdatedCustomer.Name,
            Email = UpdatedCustomer.Email,
            Phone = UpdatedCustomer.Phone
        };
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

}

