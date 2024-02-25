using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectX.Application.Usecases.Projects
{
    public class ProjectUpdateRequest
    {
      
        public Guid Id { get; set; }
        public Guid ClientID { get; set; }       
        public string? Title { get; set; }
        public bool Active { get; set; }
        [JsonIgnore]
        public string? SecretCode { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
