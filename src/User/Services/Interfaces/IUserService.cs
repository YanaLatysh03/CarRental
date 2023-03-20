using Database;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Models.Response;

namespace User.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Method for getting user information.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <returns>Response user model.</returns>
        Task<UserResponseModel> GetUserByIdAsync(Guid userId);

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
        Task<UserResponseModel> UpdateUserAsync(
            string FirstName,
            string LastName,
            string City,
            string Email,
            string Password,
            string PhoneNumber);

        Task<bool> BlockUserAsync(string email);

        /// <summary>
        /// Method for changing user role.
        /// </summary>
        /// <param name="adminId">Admin Id.</param>
        /// <param name="userEmail">User email.</param>
        /// <param name="role">Role.</param>
        /// <returns>Acknowledgment value.</returns>
        Task<bool> ChangeUserRoleAsync(string userEmail, RoleNames role);

        /// <summary>
        /// Method for invitation new User by Admin.
        /// </summary>
        /// <param name="adminId">Admin Id.</param>
        /// <param name="emailTo">Email to which to send the message.</param>
        /// <returns>Acknowledgment value.</returns>
        Task<bool> InviteDealerAsync(string emailTo);

        Task<GetUsersResponseModel> GetUsersAsync(int? page);

        Task<UserResponseModel> GetUserByEmailAsync(string email);
    }
}
