namespace User.Models.Request
{
    /// <summary>
    /// Request model class for update user information.
    /// </summary>
    public class UpdateUserRequestModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }
    }
}
