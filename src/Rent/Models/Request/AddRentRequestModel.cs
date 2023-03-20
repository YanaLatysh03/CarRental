using Database.Entities;
using Newtonsoft.Json;
using System;

namespace Rent.Models.Request
{
    public class AddRentRequestModel
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("house")]
        public int House { get; set; }

        [JsonProperty("building")]
        public int? Building { get; set; }

        [JsonProperty("appartment")]
        public int? Appartment { get; set; }

        [JsonProperty("carId")]
        public Guid CarId { get; set; }

        [JsonProperty("dateFrom")]
        public DateTime DateFrom { get; set; }

        [JsonProperty("dateTo")]
        public DateTime DateTo { get; set; }

        public UserEntity Renter { get; set; }
    }
}
