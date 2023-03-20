using System;

namespace Authorization.Models
{
    public class AccessTokenResponse
    {
        public string AccessToken { get; set; }
        public Guid UserId { get; set; }
    }
}
