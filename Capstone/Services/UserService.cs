namespace Capstone.Services;
using Capstone.Entities;
using Capstone.Repositories;
using Capstone.DTOs;

public class UserService : IuserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<User> CreateUserAsync(User newUser)
    {
        return await _userRepository.AddUserAsync(newUser);
    }

    public async Task<User?> UpdateUserAsync(int id, User updatedUser)
    {
        return await _userRepository.UpdateUserAsync(id, updatedUser);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _userRepository.DeleteUserAsync(id);
    }
}
