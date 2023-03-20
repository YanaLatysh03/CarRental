using System;
using System.Collections.Generic;

namespace Database.Entities
{
    public class CarEntity
    {
        public Guid Id { get; set; }

        public int Year { get; set; }

        public double Price { get; set; }

        public string Color { get; set; }  

        public string Brand { get; set; }

        public bool IsAccident { get; set; }

        public byte[] Image { get; set; }

        public RentAccess Access { get; set; }

        public Guid UserId { get; set; }

        public UserEntity User { get; set; }

        public List<RentEntity> Rents { get; set; }
    }

    public enum RentAccess
    {
        Free,
        Rented
    }

    public enum Color
    {
        All,
        White,
        Black,
        Red,
        Orange,
        Green,
        Grey,
        Blue
    }

    public enum Brand
    {
        All,
        BMW,
        Ford,
        Mitsubishi,
        Mercedes,
        Volkswagen,
        Opel,
        Fiat,
        Honda,
        Hendai,
        Reno,
        Shkoda
    }

    public enum Access
    {
        All,
        Rented,
        Free,
    }
}
