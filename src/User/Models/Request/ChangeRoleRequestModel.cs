using Database;
using Database.Entities;
using System;

namespace User.Models.Request
{
    /// <summary>
    /// Request model class for changing user role.
    /// </summary>
    public class ChangeRoleRequestModel
    {
        public Guid AdminId { get; set; }

        public string UserEmail { get; set; }

        public RoleNames Role { get; set; }
    }
}
