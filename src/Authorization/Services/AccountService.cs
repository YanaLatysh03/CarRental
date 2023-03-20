using Authorization.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using Authorization.Models;
using System.Text;
using System.Threading.Tasks;
using User.Models.Response;
using AutoMapper;
using MimeKit;
using MailKit.Net.Smtp;
using System.Security.Cryptography;

namespace Authorization.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IConfiguration configuration, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<AccessTokenResponse> LoginAsync(string email, string password)
        {
            var user = _accountRepository.GetUserByEmailAsync(email, password);

            if (user != null)
            {
                var identity = GetIdentity(user.Email, user.Password, user.Role.ToString());

                if (identity == null)
                {
                    throw new Exception("Invalid username or password.");
                }

                var accessToken = GenerateAccessToken(identity);

                var response = new AccessTokenResponse
                {
                    AccessToken = accessToken,
                    UserId = user.Id,
                };

                var result = await _accountRepository.SaveAccessTokenAsync(response);

                if (result)
                {
                    return response;
                }
            }

            //Shouldn't be null
            return null;
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
        public async Task<UserResponseModel> CreateUserAsync(
            string firstName,
            string lastName,
            string email,
            string password,
            string phoneNumber,
            string city)
        {

            if (string.IsNullOrEmpty(firstName)
                || string.IsNullOrEmpty(email)
                || string.IsNullOrEmpty(password)
                || string.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentException("User information is empty");
            }

            var random = new Random();
            var code = random.Next(100000, 999999);

            var user = await _accountRepository.CreateUserAsync(firstName, lastName, email, password, phoneNumber, city, code);

            var isSent = await SendCodeUserAsync(email, code);

            UserResponseModel result = null;
            if (isSent)
            {
                result = _mapper.Map<UserResponseModel>(user);
            }

            return result;
        }

        /// <summary>
        /// Method for email confirmation by code.
        /// </summary>
        /// <param name="code">Code sent by email.</param>
        /// <param name="email">User email.</param>
        /// <returns>Acknowledgment value.</returns>
        public async Task<bool> ConfirmEmailByCode(int code, string email)
        {
            var result = await _accountRepository.ConfirmEmailByCode(code, email);

            return result;
        }

        private ClaimsIdentity GetIdentity(string email, string password, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        private async Task<bool> SendCodeUserAsync(string emailTo, int code)
        {
            if (string.IsNullOrEmpty(emailTo))
            {
                throw new ArgumentNullException("Email to which to send the message is empty");
            }

            var emailmessage = new MimeMessage();

            emailmessage.From.Add(new MailboxAddress("Yana", "yanusik.latysh@mail.ru"));
            emailmessage.To.Add(new MailboxAddress("", emailTo));
            emailmessage.Subject = "Code confirmation";
            emailmessage.Body = new TextPart("Plain")
            {
                Text = $"Your code confirmation is {code}"
            };

            using var smtpClient = new SmtpClient();

            await smtpClient.ConnectAsync("smtp.mail.ru", 25);
            await smtpClient.AuthenticateAsync("yanusik.latysh@mail.ru", "egeMw4VRpdduc0iWVtiE");
            await smtpClient.SendAsync(emailmessage);
            await smtpClient.DisconnectAsync(true);

            return true;
        }

        private string GenerateAccessToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthOptions:KEY"].ToString()));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwt = new JwtSecurityToken(
                    issuer: _configuration["AuthOptions:ISSUER"].ToString(),
                    audience: _configuration["AuthOptions:AUDIENCE"].ToString(),
            notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(double.Parse(_configuration["AuthOptions:LIFETIME"]))),
                    signingCredentials: credentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomNumber);
            
            return Convert.ToBase64String(randomNumber);
        }
    }
}
