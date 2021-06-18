using System;
using System.Collections.Generic;

namespace Microservice.Database.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Identifier { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordResetGuid { get; set; }
        public DateTime? PasswordResetExpiry { get; set; }
        public int IncorrectLoginAttempts { get; set; }
        public DateTime? LockedOutUntilUtc { get; set; }
    
        public virtual List<Session> Sessions { get; set; }
    }
}