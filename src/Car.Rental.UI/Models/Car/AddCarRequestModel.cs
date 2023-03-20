using Microsoft.AspNetCore.Http;
using System;

namespace Car.Rental.UI.Models.Car
{
    public class AddCarRequestModel
    {
        public Guid DealerId { get; set; }

        public int Year { get; set; }

        public double Price { get; set; }

        public string Color { get; set; }

        public string Brand { get; set; }

        public bool IsAccident { get; set; }

        public IFormFile Image { get; set; }
    }
}
