using Car.Models.Response;

namespace Rent.Models.Response
{
    public class GetRentsResponseModel
    {
        public RentResponseModel ShippingAdress { get; set; }

        public CarResponseModel Car { get; set; }

        public RentMemberResponseModel Dealer { get; set; }
    }
}
