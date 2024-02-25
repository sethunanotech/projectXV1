using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjectX.Domain.Entities
{
    public class Project : BaseEntity
    {
        public Guid ClientID { get; set; }
        public string? Title { get; set; }        
        public string? SecretCode { get; set; }
        public bool Active { get; set; }
        [ForeignKey("ClientID")]
        [JsonIgnore]
        public virtual Client? Client { get; set; }
    }
}
