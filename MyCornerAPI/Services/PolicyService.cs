using Google;
using MyCornerAPI.Data;
using MyCornerAPI.Models.Dtos;

namespace MyCornerAPI.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly AppDbContext _db;

        public PolicyService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<PolicyDto>> GetPoliciesByUserIdAsync(int userId)
        {
            return await _db.Policies
                .Where(p => p.UserId == userId)
                .Select(p => new PolicyDto
                {
                    PolicyId = p.Id,
                    PolicyNumber = p.PolicyNumber,
                    PolicyType = p.Type,
                    Coverage = p.CoverageDescription,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Status = p.IsActive ? "Active" : "Expired"
                })
                .ToListAsync();
        }
    }
}
