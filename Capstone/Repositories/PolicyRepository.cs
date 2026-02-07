using Capstone.Entities;
using Capstone.Data;
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

            public async Task<Policy> UpdatePolicyAsync(Policy policy)
            {
                _context.Policies.Update(policy);
                await _context.SaveChangesAsync();
                return policy;
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