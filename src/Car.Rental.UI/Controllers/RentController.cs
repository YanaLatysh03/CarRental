using AutoMapper;
using Car.Models.Request;
using Car.Rental.UI.Clients;
using Car.Services.Interfaces;
using Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rent.Models.Request;
using Rent.Services.Interfaces;
using Stripe;
using System;
using System.Threading.Tasks;
using User.Services.Interfaces;

namespace Car.Rental.UI.Controllers
{
    public class RentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly RentClient _rentClient = new RentClient();
        private readonly CarClient _carClient = new CarClient();
        private readonly UserClient _userClient = new UserClient();
        private readonly int Percent = 20 / 100;

        public RentController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult RentCar(Guid carId)
        {
            ViewData["carId"] = carId;
            return View("RentCar");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] RentCarRequestModel request)
        {
            var car = await _carClient.GetCarById(request.Id);

            var paymentIntentService = new PaymentIntentService();

            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = CalculateOrderAmount(car.Car.Price),
                Currency = "USD",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            });

            return Json(new
            {
                clientSecret = paymentIntent.ClientSecret,
                payment = paymentIntent.Amount.ToString(),
                currency = paymentIntent.Currency
            });
        }

        [Authorize]
        public IActionResult GetResultPayment()
        {
            ViewData["resultPayment"] = "true";
            return View("ResultPayment");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRent([FromBody] AddRentRequestModel rent)
        {
            try
            {
                var renterEmail = User.Identity.Name;

                var renterModel = await _userClient.GetUserByEmailAsync(renterEmail);
                var renter = _mapper.Map<UserEntity>(renterModel);

                var result = _rentClient.AddRent(rent, renter);

                ViewData["resultPayment"] = "false";
                return View("ResultPayment");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRentCars()
        {
            try
            {
                var renterEmail = User.Identity.Name;

                var renter = await _userClient.GetUserByEmailAsync(renterEmail);

                var result = await _rentClient.GetRentCars(renter.Id);

                return View("RentedCars", result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangeRentStatus(Guid rentId, RentStatus status)
        {
            try
            {
                var result = await _rentClient.ChangeRentStatus(rentId, status);

                TempData["rentStatus"] = result;

                return RedirectToAction("RentedCars", "Rent");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private long CalculateOrderAmount(double price)
        {
            var result = price * Percent;

            return (long)result;
        }
    }
}
