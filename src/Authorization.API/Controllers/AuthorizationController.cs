using Authorization.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Authorization.Models.Request;

namespace Authorization.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AuthorizationController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginModel)
        {
            try
            {
                var response = await _accountService.LoginAsync(loginModel.UserName, loginModel.Password);

                return Ok(response);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequestModel user)
        {
            try
            {
                var result = await _accountService.CreateUserAsync(user.FirstName, user.LastName, user.Email, user.Password, user.PhoneNumber, user.City);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmailByCode([FromBody] ConfirmEmailRequestModel confirmEmail)
        {
            var result = await _accountService.ConfirmEmailByCode(confirmEmail.Code, confirmEmail.Email);

            return Ok(result);
        }
    }
}
