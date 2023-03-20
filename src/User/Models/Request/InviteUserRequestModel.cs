using System;

namespace User.Models.Request
{
    /// <summary>
    /// Request model class for invitation user.
    /// </summary>
    public class InviteUserRequestModel
    {
        public Guid AdminId { get; set; }

        public string EmailTo { get; set; }
    }
}
