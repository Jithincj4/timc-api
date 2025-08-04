using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimcApi.Application.DTOs;
using TimcApi.Application.Interfaces;
using TimcApi.Domain;

namespace TimcApi.Infrastructure.Services
{
    public class InMemoryFileService : IFileService
    {
        private readonly List<PatientDocument> _documents;
        private readonly string _uploadPath;

        public InMemoryFileService()
        {
            _documents = new List<PatientDocument>();
            _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            
            // Ensure upload directory exists
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        public async Task<FileDto> UploadFileAsync(Guid patientId, UploadFileRequest fileRequest)
        {
            if (fileRequest == null || fileRequest.Length == 0)
                throw new ArgumentException("File is empty or null");

            // Validate file type (PDF and common image types)
            var allowedTypes = new[] { "application/pdf", "image/jpeg", "image/png", "image/gif", "image/bmp" };
            if (!allowedTypes.Contains(fileRequest.ContentType.ToLower()))
                throw new ArgumentException("Only PDF and image files are allowed");

            // Generate unique filename
            var fileExtension = Path.GetExtension(fileRequest.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(_uploadPath, uniqueFileName);

            // Save file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileRequest.Content.CopyToAsync(stream);
            }

            // Create document entity
            var document = new PatientDocument(
                patientId,
                fileRequest.FileName,
                filePath,
                fileRequest.ContentType,
                fileRequest.Length
            );

            // Store in memory list
            _documents.Add(document);

            // Return DTO
            return new FileDto
            {
                Id = document.Id,
                PatientId = document.PatientId,
                FileName = document.FileName,
                FilePath = document.FilePath,
                ContentType = document.ContentType,
                FileSize = document.FileSize,
                UploadedAt = document.UploadedAt
            };
        }

        public async Task<IEnumerable<FileDto>> GetPatientFilesAsync(Guid patientId)
        {
            return await Task.FromResult(_documents
                .Where(d => d.PatientId == patientId)
                .Select(d => new FileDto
                {
                    Id = d.Id,
                    PatientId = d.PatientId,
                    FileName = d.FileName,
                    FilePath = d.FilePath,
                    ContentType = d.ContentType,
                    FileSize = d.FileSize,
                    UploadedAt = d.UploadedAt
                }));
        }

        public async Task<(byte[] fileContent, string contentType, string fileName)> DownloadFileAsync(string fileName)
        {
            var document = _documents.FirstOrDefault(d => Path.GetFileName(d.FilePath) == fileName);
            if (document == null)
                throw new FileNotFoundException("File not found");

            if (!File.Exists(document.FilePath))
                throw new FileNotFoundException("Physical file not found");

            var fileContent = await File.ReadAllBytesAsync(document.FilePath);
            return (fileContent, document.ContentType, document.FileName);
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            var document = _documents.FirstOrDefault(d => Path.GetFileName(d.FilePath) == fileName);
            if (document == null)
                return false;

            // Remove from memory list
            _documents.Remove(document);

            // Delete physical file
            if (File.Exists(document.FilePath))
            {
                File.Delete(document.FilePath);
            }

            return await Task.FromResult(true);
        }
    }
}