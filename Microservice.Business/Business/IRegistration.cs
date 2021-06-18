using Microservice.Business.Models;

namespace Microservice.Business.Business
{
    public interface IRegistration
    {
        int? Register(RegisterModel model, string ipAddress, out string errorMessage);
    }
}