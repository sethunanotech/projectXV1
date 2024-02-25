using Newtonsoft.Json.Linq;
using ProjectX.Application.Usecases.Login;

namespace ProjectX.WebApplication.IService
{
    public interface ILoginService
    {
        Token GenerateToken(UserLogin userLogin);
        HttpClient ToaccesAPI(string accessKey);
    }
}
