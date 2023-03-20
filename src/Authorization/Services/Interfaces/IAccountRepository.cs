using Authorization.Models;
using Database;
using Database.Entities;
using System.Threading.Tasks;

namespace Authorization.Services.Interfaces
{
    public interface IAccountRepository
    {
        UserEntity GetUserByEmailAsync(string email, string password);

        Task<bool> ConfirmEmailByCode(int code, string email);

        /// <summary>
        /// Method for creation user.
        /// </summary>
        /// <param name="firstName">Username.</param>
        /// <param name="email">User email.</param>
        /// <param name="password">User password.</param>
        /// <param name="phoneNumber">User phonenumber.</param>
        /// <param name="role">User role.</param>
        /// <returns>Response user model.</returns>
        Task<UserEntity> CreateUserAsync(
            string firstName,
            string lastName,
            string email,
            string password,
            string phoneNumber,
            string city,
            int code);

        Task<bool> SaveAccessTokenAsync(AccessTokenResponse response);
    }
}
