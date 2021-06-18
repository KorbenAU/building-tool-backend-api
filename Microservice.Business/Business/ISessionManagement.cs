namespace Microservice.Business.Business
{
    public interface ISessionManagement
    {
        bool IsSessionValid(string sessionIdentifier, string ipAddress, bool extendSession = true);
        int GetUserIdFromSessionIdentifier(string sessionIdentifier);
    }
}