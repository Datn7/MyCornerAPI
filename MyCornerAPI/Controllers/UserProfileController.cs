using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCornerAPI.Data;
using MyCornerAPI.Models;
using MyCornerAPI.Models.Dtos;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyCornerAPI.Controllers
{
    [ApiController]
    [Route("api/profile")]
    [Authorize]
    public class UserProfileController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserProfileController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/profile
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateProfile([FromBody] UserProfileDto dto)
        {
            var userId = int.Parse(User.FindFirstValue("id"));

            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
            {
                profile = new UserProfile
                {
                    UserId = userId,
                    AboutMe = dto.AboutMe,
                    Interests = dto.Interests,
                    ReactionsJson = dto.ReactionsJson,
                    ProfilePictureUrl = dto.ProfilePictureUrl,
                    CoverPhotoUrl = dto.CoverPhotoUrl
                };
                _context.UserProfiles.Add(profile);
            }
            else
            {
                profile.AboutMe = dto.AboutMe;
                profile.Interests = dto.Interests;
                profile.ReactionsJson = dto.ReactionsJson;
                profile.ProfilePictureUrl = dto.ProfilePictureUrl;
                profile.CoverPhotoUrl = dto.CoverPhotoUrl;
                _context.UserProfiles.Update(profile);
            }

            await _context.SaveChangesAsync();
            return Ok(profile);
        }

        // GET /api/profile/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var profile = await _context.UserProfiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == id);

            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        // GET /api/profile/match/{otherUserId}
        [HttpGet("match/{otherUserId}")]
        public async Task<IActionResult> GetMatchPercentage(int otherUserId)
        {
            var userId = int.Parse(User.FindFirstValue("id"));

            var myProfile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);
            var otherProfile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == otherUserId);

            if (myProfile == null || otherProfile == null)
                return NotFound(new { message = "One or both profiles not found" });

            var myInterests = myProfile.Interests?.Split(',').Select(i => i.Trim().ToLower()).ToList() ?? new List<string>();
            var otherInterests = otherProfile.Interests?.Split(',').Select(i => i.Trim().ToLower()).ToList() ?? new List<string>();

            if (!myInterests.Any() || !otherInterests.Any())
                return Ok(new { matchPercentage = 0 });

            var shared = myInterests.Intersect(otherInterests).Count();
            var total = myInterests.Union(otherInterests).Count();

            var matchPercentage = (int)(((double)shared / total) * 100);

            return Ok(new { matchPercentage });
        }
    }
}
