using ProjectX.Application.Usecases.Login;
using ProjectX.WebApplication.IService;
using System.Net.Http.Headers;

namespace ProjectX.WebApplication.Service
{
    public class LoginService : ILoginService
    {
        private readonly IHttpClientFactory _httpClient;
        public LoginService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public Token GenerateToken(UserLogin userLogin)
        {
            try
            {
                Token state = new Token();
                var baseAddress = _httpClient.CreateClient("ProjectXWebApi");
                var loginToken = baseAddress.PostAsJsonAsync("Authenticate/Login", userLogin);
                loginToken.Wait();
                var result = loginToken.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Token>();
                    readTask.Wait();
                    state = readTask.Result;
                }
                return state;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public HttpClient ToaccesAPI(string accessKey)
        {
            var httpClient = _httpClient.CreateClient("ProjectXWebApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessKey);
            return httpClient;
        }
    }
}
