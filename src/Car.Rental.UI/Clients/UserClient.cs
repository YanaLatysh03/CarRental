using System.Threading.Tasks;
using System;
using User.Models.Request;
using Database.Entities;
using User.Models.Response;
using System.Net.Http;
using Car.Rental.UI.Models.User;

namespace Car.Rental.UI.Clients
{
    public class UserClient
    {

        public UserClient()
        {
        }

        public async Task<UserResponseModel> GetUserByIdAsync(Guid userId)
        {
            try
            {
                using var client = new HttpClient();

                var result = await client.GetAsync($"http://localhost:47633/users/by-Id?userId={userId}");

                var content = await result.Content.ReadAsAsync<UserResponseModel>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserResponseModel> GetUserByEmailAsync(string email)
        {
            try
            {
                using var client = new HttpClient();

                var result = await client.GetAsync($"http://localhost:47633/users/by-email?userEmail={email}");

                var content = await result.Content.ReadAsAsync<UserResponseModel>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserResponseModel> UpdateUserAsync(string userEmail)
        {
            try
            {
                using var client = new HttpClient();

                var result = await client.GetAsync($"http://localhost:47633/users/updated?userEmail={userEmail}");

                var content = await result.Content.ReadAsAsync<UserResponseModel>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserResponseModel> UpdateUserAsync(UpdateUserRequestModel user)
        {
            try
            {
                using var client = new HttpClient();

                var result = await client.PostAsJsonAsync($"http://localhost:47633/users/update", user);

                var content = await result.Content.ReadAsAsync<UserResponseModel>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> BlockUserAsync(string email)
        {
            try
            {
                using var client = new HttpClient();

                var result = await client.GetAsync($"http://localhost:47633/users/block?email={email}");

                var content = await result.Content.ReadAsAsync<bool>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> ChangeUserRoleAsync(ChangeRoleRequestModel roleInput)
        {
            try
            {
                using var client = new HttpClient();

                var result = await client.PostAsJsonAsync($"http://localhost:47633/users/change-role", roleInput);

                var content = await result.Content.ReadAsAsync<bool>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> InviteDealer(string email)
        {
            try
            {
                using var client = new HttpClient();

                var result = await client.PostAsJsonAsync($"http://localhost:47633/users/invite", email);

                var content = await result.Content.ReadAsAsync<bool>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<GetUsersResponseModel> GetUsers(int? page)
        {
            try
            {
                using var client = new HttpClient();

                var result = await client.GetAsync($"http://localhost:47633/users/all-users?page={page}");

                var content = await result.Content.ReadAsAsync<GetUsersResponseModel>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> AddDealerAsync(string email, RoleNames role)
        {
            try
            {
                var addDealerModel = new Models.User.AddDealerRequestModel()
                {
                    Email = email,
                    Role = role
                };

                using var client = new HttpClient();

                var result = await client.PostAsJsonAsync($"http://localhost:47633/users/add-dealer", addDealerModel);

                var content = await result.Content.ReadAsAsync<bool>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
