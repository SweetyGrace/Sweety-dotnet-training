using MyLearningWebApi.Models.DTOs;
namespace MyLearningWebApi.Services;
public interface ICustomerService
{
    Task<List<CustomerResponseDto>> GetAllCustomersAsync();
    Task<CustomerResponseDto?> GetCustomerByIdAsync(int id);
    Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto customerDto);
    Task<CustomerResponseDto> UpdateCustomerAsync(int id, UpdateCustomerDto customerDto);
    Task<bool> DeleteCustomerAsync(int id);
}