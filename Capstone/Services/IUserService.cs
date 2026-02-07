using Capstone.Entities;

namespace Capstone.Services;

public interface IuserService
{

    // Define user-related service methods here
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User> CreateUserAsync(User newUser);
    Task<User?> UpdateUserAsync(int id, User updatedUser);
    Task<bool> DeleteUserAsync(int id); 
}

