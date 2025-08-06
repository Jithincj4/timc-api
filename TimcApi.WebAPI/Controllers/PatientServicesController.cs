using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimcApi.Application.DTOs;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;

namespace TimcApi.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Facilitator,SACCO")]
    public class PatientServicesController : ControllerBase
    {
        private readonly IPatientServiceRepository _repo;
        private readonly IMapper _mapper;

        public PatientServicesController(IPatientServiceRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Assign([FromBody] AssignServiceDto dto)
        {
            var entity = _mapper.Map<PatientService>(dto);
            entity.CreatedAt = DateTime.UtcNow;

            var id = await _repo.AssignAsync(entity);
            return Created("", new { id });
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            var result = await _repo.GetByPatientIdAsync(patientId);
            return Ok(_mapper.Map<IEnumerable<PatientServiceDto>>(result));
        }
    }

}
