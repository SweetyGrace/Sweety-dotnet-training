namespace Capstone.Services;
using Capstone.Entities;
using Capstone.Repositories;
using Capstone.DTOs;

public interface IEnrollmentService
{
    Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync();
    Task<Enrollment?> GetEnrollmentByIdAsync(int id);
    Task<Enrollment> AddEnrollmentAsync(Enrollment newEnrollment);
    Task<Enrollment?> UpdateEnrollmentAsync(int id, Enrollment updatedEnrollment);
    Task<IEnumerable<Enrollment>> GetEnrollmentsByUserIdAsync(int userId);
    Task<IEnumerable<Enrollment>> GetEnrollmentsByPolicyIdAsync(int policyId);
    Task<IEnumerable<Enrollment>> GetEnrollmentsByStatusAsync(string status);
    Task<Enrollment?> GetEnrollmentByUserAndPolicyAsync(int userId, int policyId);
    Task<bool> DeleteEnrollmentAsync(int id);

    Task<Enrollment?> ApproveEnrollmentAsync(int id);
    Task<Enrollment?> RejectEnrollmentAsync(int id);
}