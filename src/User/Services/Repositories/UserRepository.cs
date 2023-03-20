using AutoMapper;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Database;
using User.Models.Response;
using User.Services.Interfaces;

namespace User.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for getting user information.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <returns>Response user model.</returns>
        public async Task<UserEntity> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (result is null)
                {
                    throw new NullReferenceException("User with such ID is not found");
                }

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

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
        public async Task<UserEntity> UpdateUserAsync(
            string firstName,
            string lastName,
            string city,
            string email,
            string password,
            string phoneNumber)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user is null)
                {
                    throw new NullReferenceException("User with such email is not found");
                }

                user.FirstName = firstName;
                user.LastName = lastName;
                user.City = city;
                user.Email = email;
                user.Password = password;
                user.PhoneNumber = phoneNumber;

                await _dbContext.SaveChangesAsync();

                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method for blocking any User by Admin.
        /// </summary>
        /// <param name="adminId">Admin Id.</param>
        /// <param name="dealerEmail">Dealer email.</param>
        /// <returns>Acknowledgment value.</returns>
        public async Task<bool> BlockUserAsync(string email)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    throw new ArgumentException("User with such email is not fount");
                }

                user.Role = RoleNames.Blocked;

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method for changing user role.
        /// </summary>
        /// <param name="adminId">Admin Id.</param>
        /// <param name="userEmail">User email.</param>
        /// <param name="role">Role.</param>
        /// <returns>Acknowledgment value.</returns>
        public async Task<bool> ChangeUserRoleAsync(string userEmail, RoleNames role)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(userEmail));

                if (user is null)
                {
                    return false;
                }

                user.Role = role;

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<UserResponseModel>> GetUsersAsync(int currentPage, int pageSize)
        {
            try
            {
                var result = await _dbContext.Users.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

                List<UserResponseModel> users = new List<UserResponseModel>();

                foreach (var user in result)
                {
                    users.Add(_mapper.Map<UserResponseModel>(user));
                }

                return users;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> GetCountUsersAsync()
        {
            try
            {
                var count = await _dbContext.Users.CountAsync();

                return count;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user is null)
                {
                    throw new NullReferenceException("User with such email is not found");
                }

                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
