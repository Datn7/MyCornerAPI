using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCornerAPI.Data;
using MyCornerAPI.Models;
using MyCornerAPI.Models.Dtos;
using System.Security.Claims;

namespace MyCornerAPI.Controllers
{
    [ApiController]
    [Route("api/gallery")]
    [Authorize]
    public class GalleryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GalleryController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/gallery
        [HttpPost]
        public async Task<IActionResult> UploadGalleryItem([FromBody] GalleryItemDto dto)
        {
            var userId = int.Parse(User.FindFirstValue("id"));

            var item = new GalleryItem
            {
                UserId = userId,
                ImageUrl = dto.ImageUrl,
                Caption = dto.Caption
            };

            _context.GalleryItems.Add(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        // GET /api/gallery/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserGallery(int userId)
        {
            var items = await _context.GalleryItems
                .Where(g => g.UserId == userId)
                .ToListAsync();

            return Ok(items);
        }

        // DELETE /api/gallery/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGalleryItem(int id)
        {
            var userId = int.Parse(User.FindFirstValue("id"));
            var item = await _context.GalleryItems.FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId);

            if (item == null)
                return NotFound();

            _context.GalleryItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
