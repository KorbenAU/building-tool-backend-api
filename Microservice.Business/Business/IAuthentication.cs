using Microservice.Business.Models;

namespace Microservice.Business.Business
{
    public interface IAuthentication
    {
        string Login(LoginModel model, string ipAddress, out string errorMessage);

        void Logout(string sessionIdentifier, string ipAddress);
    }
}