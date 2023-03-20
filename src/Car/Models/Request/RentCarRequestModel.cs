using Newtonsoft.Json;
using System;

namespace Car.Models.Request
{
    public class RentCarRequestModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
