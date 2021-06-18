using System;

namespace Microservice.Database.Entities
{
    public class Session : BaseEntity
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public string SessionIdentifier { get; set; }
        public string IpAddress { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string AccessKey { get; set; }
        public DateTime? AccessKeyExpiry { get; set; }

        public virtual User User { get; set; }
    }
}