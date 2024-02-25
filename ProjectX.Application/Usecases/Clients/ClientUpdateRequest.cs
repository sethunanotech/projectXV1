using System.ComponentModel.DataAnnotations;

namespace ProjectX.Application.Usecases.Clients
{
    public class ClientUpdateRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
