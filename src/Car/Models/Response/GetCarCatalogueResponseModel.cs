namespace Car.Models.Response
{
    public class GetCarCatalogueResponseModel
    {
        public CarResponseModel Car { get; set; }

        public RentMemberResponseModel Dealer { get; set; }
    }
}
