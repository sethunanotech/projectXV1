using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProjectX.Application.Usecases.Package
{
    public class PackageUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid ProjectID { get; set; }
        public Guid EntityID { get; set; }
        public int Version { get; set; }
        public bool Active { get; set; }
        public IFormFile File { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
