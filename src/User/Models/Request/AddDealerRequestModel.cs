using Database.Entities;

namespace User.Models.Request
{
    public class AddDealerRequestModel
    {
        public string Email { get; set; }

        public RoleNames Role { get; set; }
    }
}
