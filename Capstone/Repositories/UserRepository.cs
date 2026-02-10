namespace Capstone.Repositories;
using Capstone.Entities;
using Capstone.Data;
using Microsoft.EntityFrameworkCore;
using Capstone.DTOs;
using System.Reflection.Metadata.Ecma335;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext context;
    public UserRepository(AppDbContext _context)
    {
        this.context = _context;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        // Implementation to retrieve all users
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
       var user =  await context.Users.FindAsync(id);
       if (user == null)
       {
        return null;
       }
       return user;
    }

    public async Task<User> AddUserAsync(User user)
    {
       
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateUserAsync(int id, UpdateUserDto updateDto)
    {
        var existingUser = await context.Users.FindAsync(id);
        if (existingUser == null)
        {
            return null;
        }

        // Update only the fields that are provided (not null)
        if (updateDto.Name != null)
            existingUser.Name = updateDto.Name;
        
        if (updateDto.Email != null)
            existingUser.Email = updateDto.Email;
        
        if (updateDto.PasswordHash != null)
            existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateDto.PasswordHash);
        
        if (updateDto.Role != null)
            existingUser.Role = updateDto.Role;
        
        existingUser.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return existingUser;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return false;
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}