using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimcApi.Application.DTOs;
using TimcApi.Application.Interfaces;

namespace TimcApi.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Facilitator,Admin")]
    public class MilestonesController : ControllerBase
    {
        private readonly IMilestoneService _service;

        public MilestonesController(IMilestoneService service)
        {
            _service = service;
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            var milestones = await _service.GetByPatientIdAsync(patientId);
            return Ok(milestones);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMilestoneDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByPatient), new { patientId = dto.PatientId }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMilestoneDto dto)
        {
            if (id != dto.MilestoneId) return BadRequest("ID mismatch");
            await _service.UpdateAsync(dto);
            return NoContent();
        }
    }

}
