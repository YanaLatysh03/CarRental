using System;
using System.Collections.Generic;

namespace Database.Entities
{   /// <summary>
    /// Class for table dbo.Users
    /// </summary>
    public class UserEntity
    {
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string City { get; set; }
        
        public string Email { get; set; }
       
        public string Password { get; set; }
        
        public string PhoneNumber { get; set; }

        public RoleNames Role { get; set; }

        public int Code { get; set; }

        public string RefreshToken { get; set; }

        //TODO
        public string Image { get; set; }

        public List<CarEntity> Cars { get; set; }

        public List<AccessTokenEntity> AccessTokens { get; set; }

        public List<RentEntity> Rents { get; set; }
    }

    public enum RoleNames
    {
        Admin,
        Dealer,
        User,
        Blocked
    }
}
