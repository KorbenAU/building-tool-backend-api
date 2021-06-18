using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Microservice.API.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetValueFromHeader(this HttpRequest request,
            string key)
        {
            if (string.IsNullOrEmpty(key))
                return "";

            request.Headers.TryGetValue(key, out var values);
            if (values.Any())
                return values.First();

            return "";
        }
    }
}