using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Authorization.Models.Request
{
    /// <summary>
    /// Request model class for creation user.
    /// </summary>
    public class RegisterUserRequestModel
    {
        [Required(ErrorMessage = "FirstName must be specified")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName must be specified")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email must be specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must be specified")]
        public string Password { get; set; }

        [Required(ErrorMessage = "PhoneNumber must be specified")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "City must be specified")]
        public string City { get; set; }
    }
}
