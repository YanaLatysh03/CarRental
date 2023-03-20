using AutoMapper;
using Database;
using Database.Entities;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;
using User.Models.Response;
using User.Services.Interfaces;

namespace User.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for getting user information by user Id.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <returns>Response user model.</returns>
        public async Task<UserResponseModel> GetUserByIdAsync(Guid userId)
        {

            var user = await _userRepository.GetUserByIdAsync(userId);

            var result = _mapper.Map<UserResponseModel>(user);

            return result;
        }

        /// <summary>
        /// Method for getting information by user email.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <returns>Response user model.</returns>
        public async Task<UserResponseModel> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            var result = _mapper.Map<UserResponseModel>(user);

            return result;
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
        public async Task<UserResponseModel> UpdateUserAsync(
                string firstName,
                string lastName,
                string city,
                string email,
                string password,
                string phoneNumber)
        {

            if (string.IsNullOrEmpty(firstName)
                || string.IsNullOrEmpty(lastName)
                || string.IsNullOrEmpty(city)
                || string.IsNullOrEmpty(email)
                || string.IsNullOrEmpty(password)
                || string.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentException("User information is empty");
            }

            var user = await _userRepository.UpdateUserAsync(firstName, lastName, city, email, password, phoneNumber);

            var result = _mapper.Map<UserResponseModel>(user);

            return result;
        }

        /// <summary>
        /// Method for blocking any User by Admin.
        /// </summary>
        /// <param name="adminId">Admin Id.</param>
        /// <param name="dealerEmail">Dealer email.</param>
        /// <returns>Acknowledgment value.</returns>
        public async Task<bool> BlockUserAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email is empty");
            }

            var result = await _userRepository.BlockUserAsync(email);

            return result;
        }

        /// <summary>
        /// Method for changing user role.
        /// </summary>
        /// <param name="adminId">Admin Id.</param>
        /// <param name="userEmail">User email.</param>
        /// <param name="role">Role.</param>
        /// <returns>Acknowledgment value.</returns>
        /// <exception cref="ArgumentException">If userEmail is empty.</exception>
        public async Task<bool> ChangeUserRoleAsync(string userEmail, RoleNames role)
        {
            if (string.IsNullOrEmpty(userEmail))
            {
                throw new ArgumentNullException("Email is empty");
            }

            var result = await _userRepository.ChangeUserRoleAsync(userEmail, role);

            return result;
        }

        /// <summary>
        /// Method for invitation new User by Admin.
        /// </summary>
        /// <param name="adminId">Admin Id.</param>
        /// <param name="emailTo">Email to which to send the message.</param>
        /// <returns>Acknowledgment value.</returns>
        public async Task<bool> InviteDealerAsync(string emailTo)
        {
            if (string.IsNullOrEmpty(emailTo))
            {
                throw new ArgumentNullException("Email to which to send the message is empty");
            }

            var isSent = await SendEmailToUserAsync(emailTo);

            return isSent;
        }

        public async Task<GetUsersResponseModel> GetUsersAsync(int? page)
        {
            var currentPage = page != null ? (int)page : 1;

            var userCount = await _userRepository.GetCountUsersAsync();

            var pages = new Pages(userCount, currentPage, PageSize);

            var users = await _userRepository.GetUsersAsync(pages.CurrentPage, pages.PageSize);

            var userModel = new GetUsersResponseModel
            {
                Users = users,
                Pages = pages
            };

            return userModel;
        }

        /// <summary>
        /// Method send email to new User.
        /// </summary>
        /// <param name="emailTo">Email to which to send the message.</param>
        /// <returns>Acknowledgment value.</returns>
        private async Task<bool> SendEmailToUserAsync(string emailTo)
        {
            var emailmessage = new MimeMessage();

            emailmessage.From.Add(new MailboxAddress("Yana", "yanusik.latysh@mail.ru"));
            emailmessage.To.Add(new MailboxAddress("", emailTo));
            emailmessage.Subject = "Invitation";
            emailmessage.Body = new TextPart("Plain")
            {
                Text = "We invite you to be a dealer! Contact us if you are agree."
            };

            using var smtpClient = new SmtpClient();

            await smtpClient.ConnectAsync("smtp.mail.ru", 25);
            await smtpClient.AuthenticateAsync("yanusik.latysh@mail.ru", "egeMw4VRpdduc0iWVtiE");
            await smtpClient.SendAsync(emailmessage);
            await smtpClient.DisconnectAsync(true);

            return true;
        }
    }
}
