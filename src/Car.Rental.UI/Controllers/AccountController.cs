using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Car.Rental.UI.Clients;
using Car.Rental.UI.Models.Auth;
using Authorization.Models;

namespace Car.Rental.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthorizationClient _authClient = new AuthorizationClient();

        public AccountController()
        {
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Method for user authorization.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="password">User password.</param>
        /// <returns>View</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<AccessTokenResponse> Login([FromBody] LoginRequestModel requestModel)
        {
            try
            {
                var result = await _authClient.Login(requestModel);

                //if (result.AccessToken != null)
                //{
                //    Response.Cookies.Append("AccessToken", result.AccessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                //}

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        /// <summary>
        /// Method for creation user.
        /// </summary>
        /// <param name="user">Request model.</param>
        /// <returns>Request user model.</returns>
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUserRequestModel user)
        {
            try
            {
                var result = await _authClient.RegisterUser(user);

                ViewData["Email"] = user.Email;

                return View("EnterCode");
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
        /// <returns></returns>
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmEmailByCode([FromForm] int code, string email)
        {
            var result = await _authClient.ConfirmEmailByCode(code, email);

            if (result)
            {
                return RedirectToAction("Login", "Account");
            }

            return View("EnterCode");
        }

        [Authorize]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AccessToken");
            return RedirectToAction("Login");
        }
    }
}
