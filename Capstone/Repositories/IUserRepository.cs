using Capstone.Entities;

namespace Capstone.Repositories;


public interface IUserRepository
{
    // Interface methods would be defined here
    Task <IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(int id, User user);
    Task<bool> DeleteUserAsync(int id);

    Task<User?> GetUserByEmailAsync(string email);
}