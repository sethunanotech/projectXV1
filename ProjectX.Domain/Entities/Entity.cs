using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectX.Domain.Entities
{
    public class Entity:BaseEntity
    {
        public Guid ProjectID { get; set; }
        public string? Title { get; set; }
        public bool Active { get; set; }
        public string? DisplayName { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? Argument1 { get; set; }
        public string? Argument2 { get; set; }
        public string? Argument3 { get; set; }

        [ForeignKey("ProjectID")]
        [JsonIgnore]
        public virtual Project? Project { get; set; }
    }
}
