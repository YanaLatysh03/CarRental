using System;

namespace User.Models.Response
{
    /// <summary>
    /// Response model class.
    /// </summary>
    public class UserResponseModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }
    }
}
