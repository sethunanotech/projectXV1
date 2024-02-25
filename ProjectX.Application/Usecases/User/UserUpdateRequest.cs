using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectX.Application.Usecases.User
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        [JsonIgnore]
        public string? Password { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
