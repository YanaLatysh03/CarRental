using Database.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Models.Response;

namespace User.Services.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Method for getting user information.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <returns>Response user model.</returns>
        Task<UserEntity> GetUserByIdAsync(Guid userId);

        /// <summary>
        /// Method for update user.
        /// </summary>
        /// <param name="firstName">Username.</param>
        /// <param name="lastName">Surname.</param>
        /// <param name="city">User city.</param>
        /// <param name="email">Email address.</param>
        /// <param name="password">User password.</param>
        /// <param name="phoneNumber">User phone number.</param>
        /// <returns>Response user model.</returns>
        Task<UserEntity> UpdateUserAsync(
            string firstName,
            string lastName,
            string city,
            string email,
            string password,
            string phoneNumber);

        /// <summary>
        /// Method for blocking any User by Admin.
        /// </summary>
        /// <param name="adminId">Admin Id.</param>
        /// <param name="dealerEmail">Dealer email.</param>
        /// <returns>Acknowledgment value.</returns>
        Task<bool> BlockUserAsync(string email);

        /// <summary>
        /// Method for changing user role.
        /// </summary>
        /// <param name="adminId">Admin Id.</param>
        /// <param name="userEmail">User email.</param>
        /// <param name="role">Role.</param>
        /// <returns>Acknowledgment value.</returns>
        Task<bool> ChangeUserRoleAsync(string userEmail, RoleNames role);

        Task<IEnumerable<UserResponseModel>> GetUsersAsync(int currentPage, int pageSize);

        Task<int> GetCountUsersAsync();

        Task<UserEntity> GetUserByEmailAsync(string email);
    }
}
