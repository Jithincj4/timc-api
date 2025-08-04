using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using TimcApi.Application.Interfaces;
using TimcApi.Application.DTOs;
using System.Collections.Generic;

namespace TimcApi.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Upload a file for a specific patient
        /// </summary>
        /// <param name="patientId">The patient ID</param>
        /// <param name="file">The file to upload (PDF or image)</param>
        /// <returns>File details</returns>
        /// <response code="200">Returns the uploaded file details</response>
        /// <response code="400">If no file is provided or invalid request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("upload")]
        [ProducesResponseType(typeof(FileDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile(Guid patientId, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file provided");
                }

                // Convert IFormFile to UploadFileRequest
                var fileRequest = new UploadFileRequest
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    Length = file.Length,
                    Content = file.OpenReadStream()
                };

                var fileDto = await _fileService.UploadFileAsync(patientId, fileRequest);
                return Ok(fileDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get all files for a specific patient
        /// </summary>
        /// <param name="patientId">The patient ID</param>
        /// <returns>List of patient files</returns>
        /// <response code="200">Returns the list of patient files</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("{patientId}")]
        [ProducesResponseType(typeof(IEnumerable<FileDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetPatientFiles(Guid patientId)
        {
            try
            {
                var files = await _fileService.GetPatientFilesAsync(patientId);
                return Ok(files);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Download a specific file
        /// </summary>
        /// <param name="fileName">The name of the file to download</param>
        /// <returns>File content for download</returns>
        /// <response code="200">Returns the file content</response>
        /// <response code="404">If the file is not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("download/{fileName}")]
        [ProducesResponseType(typeof(FileResult), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            try
            {
                var (fileContent, contentType, originalFileName) = await _fileService.DownloadFileAsync(fileName);
                
                return File(fileContent, contentType, originalFileName);
            }
            catch (FileNotFoundException)
            {
                return NotFound("File not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a specific file
        /// </summary>
        /// <param name="fileName">The name of the file to delete</param>
        /// <returns>Success status</returns>
        /// <response code="200">Returns success message if file was deleted</response>
        /// <response code="404">If the file is not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpDelete("delete/{fileName}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            try
            {
                var deleted = await _fileService.DeleteFileAsync(fileName);
                
                if (deleted)
                {
                    return Ok(new { message = "File deleted successfully" });
                }
                else
                {
                    return NotFound("File not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}