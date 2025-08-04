using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimcApi.Application.DTOs;

namespace TimcApi.Application.Interfaces
{
    public interface IFileService
    {
        Task<FileDto> UploadFileAsync(Guid patientId, UploadFileRequest fileRequest);
        Task<IEnumerable<FileDto>> GetPatientFilesAsync(Guid patientId);
        Task<(byte[] fileContent, string contentType, string fileName)> DownloadFileAsync(string fileName);
        Task<bool> DeleteFileAsync(string fileName);
    }
}