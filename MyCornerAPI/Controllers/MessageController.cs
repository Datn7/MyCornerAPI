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
    [Route("api/messages")]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MessageController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/messages
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto dto)
        {
            var senderId = int.Parse(User.FindFirstValue("id"));

            if (senderId == dto.ReceiverId)
                return BadRequest(new { message = "Cannot send message to yourself." });

            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = dto.ReceiverId,
                Content = dto.Content,
                Timestamp = DateTime.UtcNow
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(message);
        }

        // GET /api/messages/conversation/{userId}
        [HttpGet("conversation/{userId}")]
        public async Task<IActionResult> GetConversation(int userId)
        {
            var currentUserId = int.Parse(User.FindFirstValue("id"));

            var messages = await _context.Messages
                .Where(m =>
                    (m.SenderId == currentUserId && m.ReceiverId == userId) ||
                    (m.SenderId == userId && m.ReceiverId == currentUserId))
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            return Ok(messages);
        }
    }
}
