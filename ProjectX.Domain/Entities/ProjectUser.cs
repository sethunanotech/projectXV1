using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjectX.Domain.Entities
{
    public class ProjectUser : BaseEntity
    {
        public Guid ProjectID { get; set; }
        public Guid UserID { get; set; }
        [ForeignKey("ProjectID")]
        [JsonIgnore]
        public virtual Project? Project { get; set; }
        [ForeignKey("UserID")]
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
