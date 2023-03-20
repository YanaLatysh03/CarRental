using System;
using System.Collections.Generic;

namespace Database.Entities
{
    public class RentEntity
    {
        public Guid Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int House { get; set; }

        public int? Building { get; set; }

        public int? Appartment { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public double AdvancePayment { get; set; }

        public string Currency { get; set; }

        public bool IsAdvancePayment { get; set; }

        public RentStatus Status { get; set; }

        public Guid CarId { get; set; }

        public CarEntity Car { get; set; }

        public List<UserEntity> Users { get; set; }
    }

    public enum RentStatus
    {
        Active,
        Failed,
        Successful
    }
}
