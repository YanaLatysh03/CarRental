using Microsoft.AspNetCore.Mvc;
using Rent.Models.Request;
using Rent.Services.Interfaces;
using System.Threading.Tasks;
using System;
using Database.Entities;

namespace Rent.API.Controllers
{
    [ApiController]
    [Route("rents")]
    public class RentsController : ControllerBase
    {
        private readonly IRentService _rentService;

        public RentsController(IRentService rentService)
        {
            _rentService = rentService;
        }

        [HttpPost]
        [Route("add-rent")]
        public async Task<IActionResult> AddRent([FromBody] AddRentRequestModel rent)
        {
            try
            {
                var result = await _rentService.AddRent(rent, rent.Renter);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("rented-cars")]
        public async Task<IActionResult> GetRentCars(Guid renterId)
        {
            try
            {
                var result = await _rentService.GetRentCars(renterId);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("change-status")]
        public async Task<IActionResult> ChangeRentStatus(Guid rentId, RentStatus status)
        {
            try
            {
                var result = await _rentService.ChangeRentStatus(rentId, status);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
