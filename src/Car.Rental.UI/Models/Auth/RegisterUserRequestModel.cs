﻿namespace Car.Rental.UI.Models.Auth
{
    public class RegisterUserRequestModel
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }
       
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }
    }
}
