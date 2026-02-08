using Capstone.Entities;

namespace Capstone.Repositories;

public interface IEnrollmentRepository
{

    Task<Enrollment> AddEnrollmentAsync(Enrollment enrollment);
    Task<Enrollment?> GetEnrollmentByIdAsync(int id);
    Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync();

    Task<IEnumerable<Enrollment>> GetEnrollmentsByUserIdAsync(int userId);
    Task<IEnumerable<Enrollment>> GetEnrollmentsByPolicyIdAsync(int policyId);

    Task<IEnumerable<Enrollment>> GetEnrollmentsByStatusAsync(string status);
    Task<Enrollment?> GetEnrollmentByUserAndPolicyAsync(int userId, int policyId);
    Task<Enrollment?> UpdateEnrollmentAsync(int id, Enrollment updatedEnrollment);
    Task<bool> DeleteEnrollmentAsync(int id);
    Task<Enrollment?> ApproveEnrollmentAsync(int id);
    Task<Enrollment?> RejectEnrollmentAsync(int id);
}