using System.ComponentModel.DataAnnotations;

namespace ProjectX.Application.Usecases.Login
{
    public class UserLogin
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
