using Microsoft.EntityFrameworkCore;
using MyLearningWebApi.Models.Entities;
namespace MyLearningWebApi.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<Customer> Customers { get; set; }
}