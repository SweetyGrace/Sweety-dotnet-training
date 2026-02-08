namespace Capstone.Services;
using Capstone.Entities;
using Capstone.Repositories;
using Capstone.DTOs;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IpolicyRepository _policyRepository;

    public EnrollmentService(IEnrollmentRepository enrollmentRepository, IUserRepository userRepository, IpolicyRepository policyRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _userRepository = userRepository;
        _policyRepository = policyRepository;
    }

    public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
    {
        return await _enrollmentRepository.GetAllEnrollmentsAsync();
    }

    public async Task<Enrollment?> GetEnrollmentByIdAsync(int id)
    {
        return await _enrollmentRepository.GetEnrollmentByIdAsync(id);
    }

    public async Task<Enrollment> AddEnrollmentAsync(Enrollment newEnrollment)
    {
        return await _enrollmentRepository.AddEnrollmentAsync(newEnrollment);
    }

    public async Task<Enrollment?> UpdateEnrollmentAsync(int id, Enrollment updatedEnrollment)
    {
        return await _enrollmentRepository.UpdateEnrollmentAsync(id, updatedEnrollment);
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByUserIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new ArgumentException($"User with ID {userId} does not exist.");
        }
        return await _enrollmentRepository.GetEnrollmentsByUserIdAsync(userId);
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByPolicyIdAsync(int policyId)
    {
        var policy = await _policyRepository.GetPolicyByIdAsync(policyId);
        if (policy == null)
        {
            throw new ArgumentException($"Policy with ID {policyId} does not exist.");
        }
        return await _enrollmentRepository.GetEnrollmentsByPolicyIdAsync(policyId);
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStatusAsync(string status)
    {
        return await _enrollmentRepository.GetEnrollmentsByStatusAsync(status);
    }

    public async Task<Enrollment?> GetEnrollmentByUserAndPolicyAsync(int userId, int policyId)
    {
            var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new ArgumentException($"User with ID {userId} does not exist.");
        }
        var policy = await _policyRepository.GetPolicyByIdAsync(policyId);
        if (policy == null)
        {
            throw new ArgumentException($"Policy with ID {policyId} does not exist.");
        }
        return await _enrollmentRepository.GetEnrollmentByUserAndPolicyAsync(userId, policyId);
    }


    public async Task<bool> DeleteEnrollmentAsync(int id)
    {
        return await _enrollmentRepository.DeleteEnrollmentAsync(id);
    }

    public async Task<Enrollment?> ApproveEnrollmentAsync(int id)
    {
       return await _enrollmentRepository.ApproveEnrollmentAsync(id);
    }

    public async Task<Enrollment?> RejectEnrollmentAsync(int id)
    {
        return await _enrollmentRepository.RejectEnrollmentAsync(id);
    }

}