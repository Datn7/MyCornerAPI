using Microsoft.AspNetCore.Mvc;
using MyCornerAPI.Services;

namespace MyCornerAPI.Controllers
{
    public class PoliciesController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PoliciesController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserPolicies(int userId)
        {
            var policies = await _policyService.GetPoliciesByUserIdAsync(userId);
            return Ok(policies);
        }
    }
}
