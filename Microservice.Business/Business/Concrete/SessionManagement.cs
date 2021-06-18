using System;
using System.Linq;
using Microservice.Business.Repositories;
using Microservice.Database.Entities;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Microservice.Business.Business.Concrete
{
    public class SessionManagement : ISessionManagement
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabaseRepository<Session> _sessionRepository;

        public SessionManagement(
            IConfiguration configuration,
            IDatabaseRepository<Session> sessionRepository)
        {
            _configuration = configuration;
            _sessionRepository = sessionRepository;
        }

        public bool IsSessionValid(string sessionIdentifier, string ipAddress, bool extendSession = true)
        {
            if (string.IsNullOrEmpty(sessionIdentifier))
            {
                Log.Information("Attempted to validate a session, but no session identifier was provided by IP {0}",
                    ipAddress);
                return false;
            }

            var session = _sessionRepository.Read(m => m.SessionIdentifier.Equals(sessionIdentifier)).SingleOrDefault();

            if (session == default)
            {
                Log.Information("Attempted to validate a session with an unknown session identifier {0}",
                    sessionIdentifier);
                return false;
            }

            if (session.ExpiresAt < DateTime.UtcNow)
            {
                Log.Information(
                    "Attempted to validate a session that has expired, with an unknown session identifier {0}",
                    sessionIdentifier);
                return false;
            }

            if (!session.IpAddress.Equals(ipAddress))
            {
                Log.Warning(
                    "Attempted to use session from a different IP Address. Expecting {0}, received {1}. Terminating session.",
                    session.IpAddress, ipAddress);
                session.ExpiresAt = DateTime.UtcNow;
                _sessionRepository.Update(session);
                return false;
            }

            if (extendSession)
            {
                session.ExpiresAt = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Security:ExpiryTimeMins"));
                _sessionRepository.Update(session);
            }

            return true;
        }

        public int GetUserIdFromSessionIdentifier(string sessionIdentifier)
        {
            var session = _sessionRepository.Read(m => m.SessionIdentifier.Equals(sessionIdentifier)).SingleOrDefault();

            if (session == default)
                throw new Exception("Unknown session identifier");

            return session.UserId;
        }
    }
}