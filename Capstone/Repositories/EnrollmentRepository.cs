namespace Capstone.Repositories;
using Capstone.DTOs;
using Capstone.Entities;
using Capstone.Data;
using Microsoft.EntityFrameworkCore;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AppDbContext _context;

    public EnrollmentRepository(AppDbContext context)
    {
        _context = context;
    }

      public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
    {
        return await _context.Enrollments.ToListAsync();
    }

     public async Task<Enrollment?> GetEnrollmentByIdAsync(int id)
    {
        return await _context.Enrollments.FindAsync(id);
    }

    public async Task<Enrollment> AddEnrollmentAsync(Enrollment enrollment)
    {
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }

    public async Task<bool> DeleteEnrollmentAsync(int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment == null)
        {
            return false;
        }

        _context.Enrollments.Remove(enrollment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Enrollment?> UpdateEnrollmentAsync(int id, Enrollment updatedEnrollment)
    {
        var existingEnrollment = await _context.Enrollments.FindAsync(id);
        if (existingEnrollment == null)
        {
            return null;
        }

        _context.Enrollments.Update(updatedEnrollment);
        await _context.SaveChangesAsync();
        return updatedEnrollment;
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByUserIdAsync(int userId)
    {
        return await _context.Enrollments
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByPolicyIdAsync(int policyId)
    {
        return await _context.Enrollments
            .Where(e => e.PolicyId == policyId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStatusAsync(string status)
    {
        return await _context.Enrollments
            .Where(e => e.Status == status)
            .ToListAsync();
    }

    public async Task<Enrollment?> GetEnrollmentByUserAndPolicyAsync(int userId, int policyId)
    {
        return await _context.Enrollments
            .FirstOrDefaultAsync(e => e.UserId == userId && e.PolicyId == policyId);
    }
    public async Task<Enrollment?> ApproveEnrollmentAsync(int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment == null)
        {
            return null;
        }

        enrollment.Status = "Approved";
        _context.Enrollments.Update(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }

    public async Task<Enrollment?> RejectEnrollmentAsync(int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment == null)
        {
            return null;
        }

        enrollment.Status = "Rejected";
        _context.Enrollments.Update(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }
}