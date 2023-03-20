using System.Threading.Tasks;
using System;
using System.Net.Http;
using Authorization.Models;
using User.Models.Response;
using Car.Rental.UI.Models.Auth;

namespace Car.Rental.UI.Clients
{
    public class AuthorizationClient
    {

        public AuthorizationClient()
        {
        }

        public async Task<AccessTokenResponse> Login(LoginRequestModel loginModel)
        {
            try
            {
                using var client = new HttpClient();

                client.Timeout = TimeSpan.FromMinutes(30);

                var result = await client.PostAsJsonAsync($"http://localhost:2171/auth/signin", loginModel);

                var content = await result.Content.ReadAsAsync<AccessTokenResponse>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserResponseModel> RegisterUser(RegisterUserRequestModel user)
        {
            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(30);

                var result = await client.PostAsJsonAsync($"http://localhost:2171/auth/signup", user);

                var content = await result.Content.ReadAsAsync<UserResponseModel>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> ConfirmEmailByCode(int code, string email)
        {
            var confirmModel = new ConfirmEmailRequestModel()
            {
                Code = code,
                Email = email
            };

            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(30);

            var result = await client.PostAsJsonAsync($"http://localhost:2171/auth/confirm-email", confirmModel);

            var content = await result.Content.ReadAsAsync<bool>();

            return content;
        }
    }
}
