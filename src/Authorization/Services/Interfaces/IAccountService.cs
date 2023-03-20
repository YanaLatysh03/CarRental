using Authorization.Models;
using System.Threading.Tasks;
using User.Models.Response;

namespace Authorization.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccessTokenResponse> LoginAsync(string email, string password);

        /// <summary>
        /// Method for creation user.
        /// </summary>
        /// <param name="firstName">Username.</param>
        /// <param name="email">User email.</param>
        /// <param name="password">User password.</param>
        /// <param name="phoneNumber">User phonenumber.</param>
        /// <param name="role">User role.</param>
        /// <returns>Response user model.</returns>
        Task<UserResponseModel> CreateUserAsync(
            string firstName,
            string lastName,
            string email,
            string password,
            string phoneNumber,
            string city);

        Task<bool> ConfirmEmailByCode(int code, string email);
    }
}
