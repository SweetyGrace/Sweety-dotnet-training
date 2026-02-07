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

    public async Task<User> UpdateUserAsync(int id, User user)
    {
        var existingUser = await context.Users.FindAsync(id);
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        context.Users.Update(existingUser);
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
}