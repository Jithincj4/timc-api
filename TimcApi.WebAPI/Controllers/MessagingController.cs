using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimcApi.Application.DTOs;
using TimcApi.Application.Interfaces;

namespace TimcApi.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MessagingController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessagingController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet("thread/{userId}")]
        public async Task<IActionResult> GetConversation(int userId)
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var messages = await _service.GetConversationAsync(currentUserId, userId);
            return Ok(messages);
        }

        [HttpGet("inbox")]
        public async Task<IActionResult> GetInbox()
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var messages = await _service.GetInboxAsync(currentUserId);
            return Ok(messages);
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] SendMessageDto dto)
        {
            var senderId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var messageId = await _service.SendAsync(senderId, dto);
            return Ok(new { messageId });
        }

        [HttpPut("read/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _service.MarkAsReadAsync(id);
            return NoContent();
        }
    }

}
