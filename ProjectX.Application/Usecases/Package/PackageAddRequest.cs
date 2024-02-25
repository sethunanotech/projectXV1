using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectX.Application.Usecases.Package
{
    public class PackageAddRequest
    {
        public Guid ProjectID { get; set; }
        public Guid EntityID { get; set; }
        public int Version { get; set; }
        [DefaultValue(true)]
        public bool Active { get; set; }
        public IFormFile File { get; set; }
        public string? CreatedBy { get; set; }
    }
}
