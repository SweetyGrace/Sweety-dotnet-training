namespace Capstone.Data;

using Capstone.Entities;

// using Capstone.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext   
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Policy> Policies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
}