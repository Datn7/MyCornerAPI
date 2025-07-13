using Microsoft.AspNetCore.Mvc;

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
