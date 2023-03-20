using Database;
using System.Collections.Generic;

namespace User.Models.Response
{
    public class GetUsersResponseModel
    {
        public IEnumerable<UserResponseModel> Users { get; set; }

        public Pages Pages { get; set; }
    }
}
