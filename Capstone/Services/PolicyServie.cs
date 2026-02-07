
using Capstone.Entities;
using Capstone.Repositories;

namespace Capstone.Services
{
    public class PolicyService : IPolicyService
    {

       private readonly IpolicyRepository policyRepository;
         public PolicyService(IpolicyRepository _policyRepository)
        {
            this.policyRepository = _policyRepository;
        }
        public async Task<IEnumerable<Policy>> GetAllPoliciesAsync()
        {
            return await policyRepository.GetAllPoliciesAsync();
        }

        public async Task<Policy?> GetPolicyByIdAsync(int id)
        {
            return await policyRepository.GetPolicyByIdAsync(id);
        }

        public async Task<IEnumerable<Policy>> SearchPoliciesAsync(int minAmount, int maxAmount)
        {
            return await policyRepository.SearchPoliciesAsync(minAmount, maxAmount);
        }

        public Task<IEnumerable<Policy>> GetPolicyStatus(bool isActive)
        {
            return policyRepository.GetPolicyStatus(isActive);
        }

        public async Task<Policy> CreatePolicyAsync(Policy policy)
        {
            return await policyRepository.AddPolicyAsync(policy);
        }

        public async Task<Policy> UpdatePolicyAsync(Policy policy)
        {
            return await policyRepository.UpdatePolicyAsync(policy);
        }

        public async Task<bool> DeletePolicyAsync(int id)
        {
            return await policyRepository.DeletePolicyAsync(id);
        }


    
    }
}