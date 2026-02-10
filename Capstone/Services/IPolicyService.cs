using Capstone.Entities;
using Capstone.DTOs;

namespace Capstone.Services
{
    public interface IPolicyService
    {
      Task<IEnumerable<Policy>> GetAllPoliciesAsync();
      Task<Policy?> GetPolicyByIdAsync(int id);
      Task<IEnumerable<Policy>> SearchPoliciesAsync(int minAmount, int maxAmount);
      Task<IEnumerable<Policy>> GetPolicyStatus(bool isActive);
      Task<Policy> CreatePolicyAsync(Policy policy);

      Task<Policy?> UpdatePolicyAsync(int id, UpdatePolicyDto updateDto);
      Task<bool> DeletePolicyAsync(int id);
        
    }
}