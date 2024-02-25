
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjectX.Domain.Entities
{
    public class User : BaseEntity
    {
       
        public Guid ClientID { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        [JsonIgnore] 
        [ForeignKey("ClientID")]
        public virtual Client ?Client { get; set; }
    }
}
