using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using User.Services.Interfaces;
using User.Models.Request;

namespace User.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("by-Id")]
        public async Task<IActionResult> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var result = await _userService.GetUserByIdAsync(userId);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("by-email")]
        public async Task<IActionResult> GetUserByEmailAsync(string userEmail)
        {
            try
            {
                var result = await _userService.GetUserByEmailAsync(userEmail);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("updated")]
        public async Task<IActionResult> UpdateUserAsync(string userEmail)
        {
            try
            {
                var result = await _userService.GetUserByEmailAsync(userEmail);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequestModel user)
        {
            try
            {
                var result = await _userService.UpdateUserAsync(
                        user.FirstName,
                        user.LastName,
                        user.City,
                        user.Email,
                        user.Password,
                        user.PhoneNumber);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("block")]
        public async Task<IActionResult> BlockUserAsync(string email)
        {
            try
            {
                var result = await _userService.BlockUserAsync(email);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("change-role")]
        public async Task<IActionResult> ChangeUserRoleAsync([FromBody] ChangeRoleRequestModel roleInput)
        {
            try
            {
                var result = await _userService.ChangeUserRoleAsync(roleInput.UserEmail, roleInput.Role);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("invite")]
        public async Task<IActionResult> InviteDealer([FromBody] string email)
        {
            try
            {
                var result = await _userService.InviteDealerAsync(email);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("all-users")]
        public async Task<IActionResult> GetUsers(int? page)
        {
            try
            {
                var result = await _userService.GetUsersAsync(page);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("add-dealer")]
        public async Task<IActionResult> AddDealerAsync([FromBody] AddDealerRequestModel addDealerModel)
        {
            try
            {
                var result = await _userService.ChangeUserRoleAsync(addDealerModel.Email, addDealerModel.Role);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
