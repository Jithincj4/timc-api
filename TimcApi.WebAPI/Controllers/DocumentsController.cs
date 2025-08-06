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
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _service;

        public DocumentsController(IDocumentService service)
        {
            _service = service;
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            var result = await _service.GetByPatientAsync(patientId);
            return Ok(result);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadDocumentDto dto)
        {
            var uploadedBy = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var fileName = await _service.UploadAsync(dto, uploadedBy);
            return Ok(new { fileName });
        }

        [HttpGet("file/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var result = await _service.DownloadAsync(id);
            if (result == null) return NotFound();
            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Facilitator,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }

}
