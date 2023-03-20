using System.ComponentModel.DataAnnotations;

namespace CarRentalUIBlazor.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
