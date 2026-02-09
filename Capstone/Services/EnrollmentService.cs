namespace Capstone.Services;
using Capstone.Entities;
using Capstone.Repositories;
using Capstone.DTOs;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IpolicyRepository _policyRepository;
    private readonly ILogger<EnrollmentService> _logger;

    public EnrollmentService(IEnrollmentRepository enrollmentRepository, IUserRepository userRepository, IpolicyRepository policyRepository, ILogger<EnrollmentService> logger)
    {
        _enrollmentRepository = enrollmentRepository;
        _userRepository = userRepository;
        _policyRepository = policyRepository;
        _logger = logger;
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
        _logger.LogInformation("Creating enrollment - UserId: {UserId}, PolicyId: {PolicyId}", newEnrollment.UserId, newEnrollment.PolicyId);
        var result = await _enrollmentRepository.AddEnrollmentAsync(newEnrollment);
        _logger.LogInformation("Enrollment created successfully - EnrollmentId: {EnrollmentId}", result.Id);
        return result;
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
        _logger.LogInformation("Approving enrollment - EnrollmentId: {EnrollmentId}", id);
        var result = await _enrollmentRepository.ApproveEnrollmentAsync(id);
        if (result != null)
        {
            _logger.LogInformation("Enrollment approved - EnrollmentId: {EnrollmentId}, UserId: {UserId}", id, result.UserId);
        }
        return result;
    }

    public async Task<Enrollment?> RejectEnrollmentAsync(int id)
    {
        _logger.LogInformation("Rejecting enrollment - EnrollmentId: {EnrollmentId}", id);
        var result = await _enrollmentRepository.RejectEnrollmentAsync(id);
        if (result != null)
        {
            _logger.LogInformation("Enrollment rejected - EnrollmentId: {EnrollmentId}, UserId: {UserId}", id, result.UserId);
        }
        return result;
    }

}