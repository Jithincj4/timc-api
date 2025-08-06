using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TimcApi.Application.DTOs;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public DocumentService(IDocumentRepository repo, IMapper mapper, IWebHostEnvironment env)
        {
            _repo = repo;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IEnumerable<DocumentDto>> GetByPatientAsync(int patientId)
        {
            var result = await _repo.GetByPatientIdAsync(patientId);
            return _mapper.Map<IEnumerable<DocumentDto>>(result);
        }

        public async Task<string> UploadAsync(UploadDocumentDto dto, int uploadedBy)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{dto.File.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            var doc = new Document
            {
                PatientId = dto.PatientId,
                FileName = dto.File.FileName,
                FilePath = uniqueFileName,
                FileType = dto.File.ContentType,
                UploadedAt = DateTime.UtcNow,
                UploadedBy = uploadedBy,
                Description = dto.Description,
                Stage = dto.Stage
            };

            await _repo.CreateAsync(doc);
            return uniqueFileName;
        }

        public async Task<FileStreamResult?> DownloadAsync(int id)
        {
            var doc = await _repo.GetByIdAsync(id);
            if (doc == null) return null;

            var path = Path.Combine(_env.WebRootPath, "uploads", doc.FilePath);
            if (!File.Exists(path)) return null;

            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(stream, doc.FileType)
            {
                FileDownloadName = doc.FileName
            };
        }

        public async Task DeleteAsync(int id)
        {
            var doc = await _repo.GetByIdAsync(id);
            if (doc != null)
            {
                var path = Path.Combine(_env.WebRootPath, "uploads", doc.FilePath);
                if (File.Exists(path)) File.Delete(path);
            }

            await _repo.DeleteAsync(id);
        }
    }

}
