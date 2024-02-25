using System.ComponentModel.DataAnnotations;

namespace ProjectX.Application.Usecases.Clients
{
    public class ClientAddRequest
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? CreatedBy { get; set; }
    }
}
