using System.ComponentModel.DataAnnotations;

namespace ProjectX.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}
