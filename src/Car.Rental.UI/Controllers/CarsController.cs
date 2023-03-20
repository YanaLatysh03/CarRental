using Car.Models.Response;
using Car.Rental.UI.Clients;
using Car.Rental.UI.Models.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car.Rental.UI.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarClient _carClient = new CarClient();
        private readonly UserClient _userClient = new UserClient();

        public CarsController()
        {
        }

        [HttpGet]
        public async Task<CreatePaginationResponseModel> GetCarCatalogue(int? page)
        {
            try
            {
                var result = await _carClient.GetCarCatalogue(page);

                return result;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Dealer, Admin")]
        public IActionResult AddCar()
        {
            return View("AddCar");
        }

        [HttpPost]
        [Authorize(Roles = "Dealer")]
        public async Task<IActionResult> AddCar([FromForm] AddCarRequestModel carModel)
        {
            try
            {
                //var dealerEmail = User.Identity.Name;

                //var dealer = await _userClient.GetUserByEmailAsync(dealerEmail);

                var result = await _carClient.AddCar(carModel, new Guid("9485DAEE-3CD1-4EDD-9CF4-2FCE6A7E2A88"));

                TempData["successAdd"] = result;

                return View("AddCar");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCarById(Guid carId)
        {
            try
            {
                var result = await _carClient.GetCarById(carId);

                return View("CarDetails", result);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetCarsByFilter([FromForm] CreatePaginationResponseModel paginationModel)
        {
            try
            {
                var result = await _carClient.GetCarsByFilter(paginationModel.Filter);

                return View("CarCatalogue", result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IActionResult> GetDealerCars()
        {
            try
            {
                var dealerEmail = User.Identity.Name;

                var dealer = await _userClient.GetUserByEmailAsync(dealerEmail);

                var result = await _carClient.GetDealerCars(dealer.Id);

                return View("DealerCars", result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
