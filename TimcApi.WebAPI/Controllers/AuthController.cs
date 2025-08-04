using Microsoft.AspNetCore.Mvc;
using TimcApi.Application.DTOs;
using TimcApi.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace TimcApi.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public AuthController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        /// <summary>
        /// Registers a new patient in the system
        /// </summary>
        /// <param name="registerDto">Patient registration information</param>
        /// <returns>The newly registered patient information</returns>
        /// <response code="201">Returns the newly registered patient</response>
        /// <response code="400">If the registration data is invalid or email already exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(PatientResponseDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PatientResponseDto>> Register([FromBody] RegisterPatientDto registerDto)
        {
            try
            {
                var result = await _patientService.RegisterAsync(registerDto);
                return CreatedAtAction(nameof(Register), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during registration" });
            }
        }

        /// <summary>
        /// Authenticates a patient and returns access and refresh tokens
        /// </summary>
        /// <param name="loginDto">Patient login credentials</param>
        /// <returns>Authentication tokens and expiry information</returns>
        /// <response code="200">Returns access and refresh tokens</response>
        /// <response code="401">If the credentials are invalid</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var response = await _patientService.LoginAsync(loginDto);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during login" });
            }
        }

        /// <summary>
        /// Refreshes an access token using a valid refresh token
        /// </summary>
        /// <param name="refreshTokenDto">Refresh token request</param>
        /// <returns>New access and refresh tokens</returns>
        /// <response code="200">Returns new tokens</response>
        /// <response code="401">If the refresh token is invalid or expired</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(LoginResponseDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<LoginResponseDto>> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            try
            {
                var response = await _patientService.RefreshTokenAsync(refreshTokenDto);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during token refresh" });
            }
        }

        /// <summary>
        /// Revokes a specific refresh token
        /// </summary>
        /// <param name="refreshTokenDto">Refresh token to revoke</param>
        /// <returns>Success message</returns>
        /// <response code="200">Token revoked successfully</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("revoke")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> RevokeToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            try
            {
                await _patientService.RevokeTokenAsync(refreshTokenDto);
                return Ok(new { message = "Token revoked successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during token revocation" });
            }
        }

        /// <summary>
        /// Revokes all refresh tokens for a specific patient
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <returns>Success message</returns>
        /// <response code="200">All tokens revoked successfully</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("revoke-all/{patientId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> RevokeAllTokens(Guid patientId)
        {
            try
            {
                await _patientService.RevokeAllTokensAsync(patientId);
                return Ok(new { message = "All tokens revoked successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during token revocation" });
            }
        }
    }
}