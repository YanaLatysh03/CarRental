using Database.Entities;
using System;
using System.Collections.Generic;

namespace Car.Models.Response
{
    public class RentResponseModel
    {
        public Guid Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public List<int> House { get; set; }

        public int? Building { get; set; }

        public int? Appartment { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public bool IsAdvancePayment { get; set; }

        public RentStatus Status { get; set; }

        public List<RentMemberResponseModel> Users { get; set; }
    }
}
