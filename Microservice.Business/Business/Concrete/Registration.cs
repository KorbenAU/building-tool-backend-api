using System;
using Microservice.Business.Extensions;
using Microservice.Business.Models;
using Microservice.Business.Repositories;
using Microservice.Database.Entities;
using Serilog;

namespace Microservice.Business.Business.Concrete
{
    public class Registration : IRegistration
    {
        private readonly IDatabaseRepository<User> _userRepository;

        public Registration(IDatabaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public int? Register(RegisterModel model, string ipAddress, out string errorMessage)
        {
            if (model.EmailAddress.IsNullOrEmpty() || model.Password.IsNullOrEmpty())
            {
                Log.Information("Registration attempted with empty email address or password from IP {0}", ipAddress);
                errorMessage = Constants.EmptyUsernameOrPasswordInRegister;
                return null;
            }

            if (!model.EmailAddress.IsValidEmail())
            {
                Log.Information("Registration attempted with invalid email address format from IP {0}", ipAddress);
                errorMessage = Constants.InvalidEmailAddressFormat;
                return null;
            }

            // check if we have an account by this email address
            var emailExists = _userRepository.Count(m => m.EmailAddress.Equals(model.EmailAddress)) > 0;

            var usernameExists = !model.Username.IsNullOrEmpty()
                                 &&
                                 _userRepository.Count(m => m.Username.Equals(model.Username)) > 0;

            if (emailExists || usernameExists)
            {
                Log.Information("Registration attempted email address or username that is already in use from IP {0}",
                    ipAddress);
                errorMessage = Constants.EmailAlreadyInUse;
                return null;
            }

            // check password policy
            if (!model.Password.MeetsPasswordPolicy())
            {
                Log.Information(
                    "Registration attempted with password that does not meet the passwod policy from IP {0}",
                    ipAddress);
                errorMessage = Constants.PasswordNotMeetComplexity;
                return null;
            }

            var passwordSalt = Services.Encryption.Hash.CreateSalt();
            var passwordHash = Services.Encryption.Hash.GenerateHash(model.Password, passwordSalt);

            var user = new User()
            {
                EmailAddress = model.EmailAddress,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordHash = passwordHash,
                Identifier = Guid.NewGuid().ToString().ToLower(),
                PasswordSalt = passwordSalt,
                IncorrectLoginAttempts = 0,
                LockedOutUntilUtc = null
            };

            if (!model.Username.IsNullOrEmpty()) user.Username = model.Username;

            _userRepository.Create(user);

            Log.Information("Registration started for email address {0} from IP {1}", model.EmailAddress, ipAddress);

            errorMessage = null;
            return user.Id;
        }
    }
}