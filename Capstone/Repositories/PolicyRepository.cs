using Capstone.Entities;
using Capstone.Data;
using Capstone.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Repositories
{
    public class PolicyRepository: IpolicyRepository
    {
          private readonly AppDbContext _context;
          public PolicyRepository(AppDbContext context)
          {
            _context = context;
          }

    public async Task<IEnumerable<Policy>> GetAllPoliciesAsync()
        {
            return await _context.Policies.ToListAsync();
        }

        public async Task<Policy?> GetPolicyByIdAsync(int id)
        {
            var policy = await _context.Policies.FindAsync(id);
            return policy;
        }

        public async Task<IEnumerable<Policy>> SearchPoliciesAsync(int minAmount, int maxAmount)
        {
            return await _context.Policies
                .Where(p => p.PremiumAmount >= minAmount && p.PremiumAmount <= maxAmount)
                .ToListAsync();
        }

        public async Task<IEnumerable<Policy>> GetPolicyStatus(bool isActive)
        {
            // Placeholder implementation
            return await _context.Policies
                .Where(p => p.IsActive == isActive)
                .ToListAsync();
        }

            public async Task<Policy> AddPolicyAsync(Policy policy)
            {
                _context.Policies.Add(policy);
                await _context.SaveChangesAsync();
                return policy;
            }

            public async Task<Policy?> UpdatePolicyAsync(int id, UpdatePolicyDto updateDto)
            {
                var existingPolicy = await _context.Policies.FindAsync(id);
                if (existingPolicy == null)
                {
                    return null;
                }

                // Update only the fields that are provided (not null)
                if (updateDto.PolicyName != null)
                    existingPolicy.PolicyName = updateDto.PolicyName;
                
                if (updateDto.PolicyDescription != null)
                    existingPolicy.PolicyDescription = updateDto.PolicyDescription;
                
                if (updateDto.PremiumAmount.HasValue)
                    existingPolicy.PremiumAmount = (int)updateDto.PremiumAmount.Value;
                
                if (updateDto.IsActive.HasValue)
                    existingPolicy.IsActive = updateDto.IsActive.Value;

                await _context.SaveChangesAsync();
                return existingPolicy;
            }

            public async Task<bool> DeletePolicyAsync(int id)
            {
                var policy = await _context.Policies.FindAsync(id);
                if (policy == null)
                {
                    return false;
                }
                _context.Policies.Remove(policy);
                await _context.SaveChangesAsync();
                return true;
            }



    }
}