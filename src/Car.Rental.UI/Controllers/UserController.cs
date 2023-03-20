using Car.Rental.UI.Clients;
using Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using User.Models.Request;
using User.Models.Response;

namespace Car.Rental.UI.Controllers
{
    /// <summary>
    /// User controller.
    /// </summary>
    public class UserController : Controller
    {
        private readonly UserClient _userClient = new UserClient();

        public UserController()
        {
        }

        /// <summary>
        /// Method for getting user information.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <returns>Output user model.</returns>
        [HttpGet]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid userId)
        {
            try
            {
                var result = await _userClient.GetUserByIdAsync(userId);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers(int? page)
        {
            try
            {
                var users = await _userClient.GetUsers(page);

                return View("GetUsers", users);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<UserResponseModel> UpdateUserAsync()
        {
            try
            {
                var userEmail = User.Identity.Name;

                var result = await _userClient.UpdateUserAsync(userEmail);

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
        /// <param name="userForUpdate">Input model.</param>
        /// <returns>Output user model.</returns>
        [HttpPost]
        [Authorize]
        public async Task<UserResponseModel> UpdateUserAsync([FromBody] UpdateUserRequestModel user)
        {
            try
            {
                var result = await _userClient.UpdateUserAsync(user);

                //if (result != null)
                //{
                //    TempData["UpdateData"] = true;
                //}
                //else
                //{
                //    TempData["UpdateData"] = false;
                //}

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method for blocking any User by Admin.
        /// </summary>
        /// <param name="inputModel">Input model.</param>
        /// <returns>Acknowledgment value.</returns>
        /// <exception cref="Exception"></exception>
        [Authorize(Roles = "Admin")]
        [Route("block-user")]
        public async Task<IActionResult> BlockUserAsync([FromQuery] string email)
        {
            try
            {
                var result = await _userClient.BlockUserAsync(email);

                TempData["successBlock"] = result;

                return RedirectToAction("GetUsers", "User");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method for changing user role.
        /// </summary>
        /// <param name="roleInput">Input model.</param>
        /// <returns>Acknowledgment value.</returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [Route("change-user-role")]
        public async Task<IActionResult> ChangeUserRoleAsync([FromBody] ChangeRoleRequestModel roleInput)
        {
            try
            {
                var result = await _userClient.ChangeUserRoleAsync(roleInput);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult InviteDealer()
        {
            return View("InvitationDealer");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> InviteDealer([FromForm] string email)
        {
            try
            {
                var result = await _userClient.InviteDealer(email);

                if (result)
                {
                    TempData["InviteSuccess"] = true;
                }

                return View("InvitationDealer");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddDealer()
        {
            return View("AddingDealer");
        }

        [HttpPost]
        public async Task<IActionResult> AddDealerAsync([FromForm] string email)
        {
            try
            {
                var result = await _userClient.AddDealerAsync(email, RoleNames.Dealer);

                TempData["AddDealer"] = result;

                return View("AddingDealer");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
