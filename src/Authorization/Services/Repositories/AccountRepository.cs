using System;
using Authorization.Services.Interfaces;
using System.Linq;
using Database.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Authorization.Models;
using Authorization.Database;

namespace Authorization.Services.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationContext _dbContext;

        public AccountRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserEntity GetUserByEmailAsync(string email, string password)
        {
            try
            {
                var result = _dbContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

                if (result is null)
                {
                    throw new ArgumentException("User with such email is not found.");
                }

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method for creation user.
        /// </summary>
        /// <param name="firstName">Username.</param>
        /// <param name="email">User email.</param>
        /// <param name="password">User password.</param>
        /// <param name="phoneNumber">User phonenumber.</param>
        /// <param name="role">User role.</param>
        /// <returns>Response user model.</returns>
        public async Task<UserEntity> CreateUserAsync(
            string firstName,
            string lastName,
            string email,
            string password,
            string phoneNumber,
            string city,
            int code)
        {
            try
            {
                Guid guid = Guid.NewGuid();

                var newUser = new UserEntity
                {
                    Id = guid,
                    FirstName = firstName,
                    LastName = lastName,
                    City = city,
                    Email = email,
                    Password = password,
                    PhoneNumber = phoneNumber,
                    Role = RoleNames.Admin,
                    Code = code
                };

                await _dbContext.Users.AddAsync(newUser);
                await _dbContext.SaveChangesAsync();

                return newUser;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method for email confirmation by code.
        /// </summary>
        /// <param name="code">Code sent by email.</param>
        /// <param name="email">User email.</param>
        /// <returns>Acknowledgment value.</returns>
        public async Task<bool> ConfirmEmailByCode(int code, string email)
        {
            try
            {
                var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Code == code && u.Email == email);

                if (result is null)
                {
                    throw new NullReferenceException("User with such code and email is not found");
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> SaveAccessTokenAsync(AccessTokenResponse response)
        {
            try
            {
                AccessTokenEntity newToken = new()
                {
                    Token = response.AccessToken,
                    UserId = response.UserId
                };

                await _dbContext.AccessTokens.AddAsync(newToken);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
