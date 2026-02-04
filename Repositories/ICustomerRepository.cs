using MyLearningWebApi.Models.Entities;

namespace MyLearningWebApi.Repositories;
public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task<Customer> CreateAsync(Customer customer);
    Task<Customer> UpdateAsync(Customer customer);
    Task<bool> DeleteAsync(int id);
     Task<bool> ExistsAsync(int id);

}