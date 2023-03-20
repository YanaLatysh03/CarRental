using System.Collections.Generic;

namespace Car.Models.Response
{
    public class GetDealerCarsResponseModel
    {
        public CarResponseModel Car { get; set; }
       
        public List<RentResponseModel> Rents { get; set; }
    }
}
