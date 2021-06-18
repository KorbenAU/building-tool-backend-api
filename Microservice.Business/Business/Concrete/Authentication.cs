using System;
using System.Linq;
using Microservice.Business.Extensions;
using Microservice.Business.Models;
using Microservice.Business.Repositories;
using Microservice.Database.Entities;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Microservice.Business.Business.Concrete
{
    public class Authentication : IAuthentication
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabaseRepository<User> _userRepository;
        private readonly IDatabaseRepository<Session> _sessionRepository;

        public Authentication(
            IConfiguration configuration,
            IDatabaseRepository<User> userRepository,
            IDatabaseRepository<Session> sessionRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
        }

        public string Login(LoginModel model, string ipAddress, out string errorMessage)
        {
            if (model.Username.IsNullOrEmpty() || model.Password.IsNullOrEmpty())
            {
                errorMessage = Constants.EmptyUsernameOrPassword;
                return null;
            }

            var user = _userRepository.ReadOne(m =>
                m.Username.IsNotNullOrEmpty() && m.Username.Equals(model.Username) ||
                m.EmailAddress.Equals(model.Username));

            if (user == default)
            {
                errorMessage = Constants.InvalidUsername;
                return null;
            }

            if (user.LockedOutUntilUtc > DateTime.UtcNow)
            {
                errorMessage = Constants.AccountLockedOutMessage;
                return null;
            }

            if (!Services.Encryption.Hash.AreEqual(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                user.IncorrectLoginAttempts++;
                _userRepository.Update(user);

                if (user.IncorrectLoginAttempts >= _configuration.GetValue<int>("Security:MaxIncorrectLoginAttempts"))
                {
                    Log.Information(
                        "User ID#{0} has attempted to login too many times incorrectly. Account will now be locked out.",
                        user.Id);
                    user.LockedOutUntilUtc = DateTime.UtcNow.AddMinutes(
                        _configuration.GetValue<int>("Security:LockoutDurationMins"));

                    _userRepository.Update(user);

                    errorMessage = Constants.AccountLockedOutMessage;
                }
                else
                    errorMessage = Constants.InvalidPassword;

                return null;
            }

            if (user.LockedOutUntilUtc != null ||
                user.IncorrectLoginAttempts != 0)
            {
                Log.Information("Resetting lockout information for user ID#{0} due to successful login", user.Id);
                user.LockedOutUntilUtc = null;
                user.IncorrectLoginAttempts = 0;
                _userRepository.Update(user);
            }

            Log.Information("Creating session for user ID#{0} at IP address {1}", user.Id, ipAddress);
            var session = new Session()
            {
                UserId = user.Id,
                SessionIdentifier = Guid.NewGuid().ToString().ToLower(),
                IpAddress = ipAddress,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Security:ExpiryTimeMins"))
            };
            _sessionRepository.Create(session);

            errorMessage = null;
            return session.SessionIdentifier;
        }

        public void Logout(string sessionIdentifier, string ipAddress)
        {
            var sessions = _sessionRepository.Read(m =>
                m.SessionIdentifier.Equals(sessionIdentifier)
                && m.IpAddress.Equals(ipAddress)
                && m.ExpiresAt > DateTime.UtcNow).ToList();

            foreach (var session in sessions)
            {
                Log.Information("Killing session with ID {0}, due to user request", session.Id);
                session.ExpiresAt = DateTime.UtcNow;
                _sessionRepository.Update(session);
            }
        }
    }
}