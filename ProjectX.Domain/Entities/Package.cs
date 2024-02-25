using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjectX.Domain.Entities
{
    public class Package : BaseEntity
    {
        public Guid ProjectID { get; set; }
        public Guid EntityID { get; set; }

        public int Version { get; set; }
        public bool Active { get; set; }
        public string? Url { get; set; }
        [JsonIgnore]
        [ForeignKey("ProjectID")]
        public virtual Project? Project { get; set; }

        [JsonIgnore]
        [ForeignKey("EntityID")]
        public virtual Entity? Entity { get; set; }
    }
}
