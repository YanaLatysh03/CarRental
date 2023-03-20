using Database.Entities;

namespace Car.Rental.UI.Models.User
{
    public class AddDealerRequestModel
    {
        public string Email { get; set; }

        public RoleNames Role { get; set; }
    }
}
