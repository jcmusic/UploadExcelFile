using Microsoft.AspNetCore.Http;

namespace Domain.Models
{
    public interface IFileUploadRequest
    {
        IFormFile File { get; set; }
        int OrganizationId { get; set; }
    }

    public class FileUploadRequest : IFileUploadRequest
    {
        public int OrganizationId { get; set; }
        public IFormFile File { get; set; }
    }
}