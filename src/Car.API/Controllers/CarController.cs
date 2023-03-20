using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Car.Services.Interfaces;
using Car.Models.Request;
using Car.Models.Response;

namespace Car.API.Controllers
{
    [ApiController]
    [Route("car")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        [Route("catalogue")]
        public async Task<IActionResult> GetCarCatalogue(int? page)
        {
            try
            {
                var result = await _carService.GetCarCatalogue(page);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("add-car")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> AddCar([FromBody] AddCarRequestModel carModel)
        {
            try
            {
                var result = await _carService.AddCar(carModel, carModel.DealerId);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("by-Id")]
        public async Task<IActionResult> GetCarById(Guid carId)
        {
            try
            {
                var result = await _carService.GetCarById(carId);

                return Ok(result);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("by-filter")]
        public async Task<IActionResult> GetCarsByFilter([FromBody] ApplyFilterRequestModel filters)
        {
            try
            {
                var result = await _carService.GetCarsByFilter(filters);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("dealer-cars")]
        public async Task<IActionResult> GetDealerCars(Guid dealerId)
        {
            try
            {
                var result = await _carService.GetDealerCars(dealerId);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
