namespace ProjectX.Application.Usecases.Login
{
    public class Token
    {
        public string? AccessToken { get; set; }
        public int TokenExpiresInMinutes { get; set; }
    }
}
