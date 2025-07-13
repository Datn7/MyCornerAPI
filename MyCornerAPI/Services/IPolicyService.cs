using MyCornerAPI.Models.Dtos;

namespace MyCornerAPI.Services
{
    public interface IPolicyService
    {
        Task<List<PolicyDto>> GetPoliciesByUserIdAsync(int userId);
    }
}
