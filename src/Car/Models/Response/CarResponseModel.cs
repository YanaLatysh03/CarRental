using Database.Entities;
using System;

namespace Car.Models.Response
{
    public class CarResponseModel
    {
        public Guid Id { get; set; }

        public int Year { get; set; }

        public double Price { get; set; }

        public string Color { get; set; }

        public string Brand { get; set; }

        public bool IsAccident { get; set; }

        public RentAccess Access { get; set; }

        public byte[] Image { get; set; }
    }
}
