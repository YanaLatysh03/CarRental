using System;

namespace Database.Entities
{
    public class AccessTokenEntity
    {
        public Guid Id { get; set; }

        public string Token { get; set; }

        public Guid UserId { get; set; }

        public UserEntity User { get; set; }
    }
}
