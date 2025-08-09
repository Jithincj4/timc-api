using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimcApi.Application.DTOs;
using TimcApi.Application.Interfaces;

namespace TimcApi.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class SaccosController : ControllerBase
    {
        private readonly ISaccoService _service;
        private readonly IUserService _userService;

        public SaccosController(ISaccoService service,IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSaccoDto dto)
        {

            var usrDetails = new CreateUserDto()
            {

                Username = dto.UserName,
                Password = dto.Password,
                Email=dto.Email,
                RoleId = 2//TODO: Creature role enum
            };

            var saccoUserId = await _userService.CreateAsync(usrDetails);
            dto.UserId = saccoUserId;

            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.GetByEmailAsync(email);
            var userId = user?.UserId ?? 0;

            var id = await _service.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSaccoDto dto)
        {
            if (id != dto.AgentId) return BadRequest("ID mismatch");
            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }

}
