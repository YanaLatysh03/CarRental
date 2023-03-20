using AutoMapper;
using Car.Models.Response;
using Database.Entities;
using Rent.Models.Response;
using User.Models.Response;

namespace Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserResponseModel>();
            CreateMap<UserResponseModel, UserEntity>();

            CreateMap<CarEntity, GetCarCatalogueResponseModel>()
                .ForPath(c => c.Car.Id, src => src.MapFrom(s => s.Id))
                .ForPath(c => c.Car.Year, src => src.MapFrom(s => s.Year))
                .ForPath(c => c.Car.Price, src => src.MapFrom(s => s.Price))
                .ForPath(c => c.Car.Color, src => src.MapFrom(s => s.Color))
                .ForPath(c => c.Car.Brand, src => src.MapFrom(s => s.Brand))
                .ForPath(c => c.Car.IsAccident, src => src.MapFrom(s => s.IsAccident))
                .ForPath(c => c.Car.Access, src => src.MapFrom(s => s.Access))
                .ForPath(c => c.Car.Image, src => src.MapFrom(s => s.Image))
                .ForPath(c => c.Dealer.FirstName, src => src.MapFrom(s => s.User.FirstName))
                .ForPath(c => c.Dealer.LastName, src => src.MapFrom(s => s.User.LastName))
                .ForPath(c => c.Dealer.City, src => src.MapFrom(s => s.User.City))
                .ForPath(c => c.Dealer.Email, src => src.MapFrom(s => s.User.Email))
                .ForPath(c => c.Dealer.PhoneNumber, src => src.MapFrom(s => s.User.PhoneNumber));

            CreateMap<RentEntity, GetRentsResponseModel>()
                .ForPath(c => c.ShippingAdress.Id, src => src.MapFrom(s => s.Id))
                .ForPath(c => c.ShippingAdress.Country, src => src.MapFrom(s => s.Country))
                .ForPath(c => c.ShippingAdress.City, src => src.MapFrom(s => s.City))
                .ForPath(c => c.ShippingAdress.Street, src => src.MapFrom(s => s.Street))
                .ForPath(c => c.ShippingAdress.House, src => src.MapFrom(s => s.House))
                .ForPath(c => c.ShippingAdress.Building, src => src.MapFrom(s => s.Building))
                .ForPath(c => c.ShippingAdress.Appartment, src => src.MapFrom(s => s.Appartment))
                .ForPath(c => c.ShippingAdress.DateFrom, src => src.MapFrom(s => s.DateFrom))
                .ForPath(c => c.ShippingAdress.Status, src => src.MapFrom(s => s.Status))
                .ForPath(c => c.ShippingAdress.DateTo, src => src.MapFrom(s => s.DateTo))
                .ForPath(c => c.ShippingAdress.IsAdvancePayment, src => src.MapFrom(s => s.IsAdvancePayment))
                .ForPath(c => c.Car.Id, src => src.MapFrom(s => s.Car.Id))
                .ForPath(c => c.Car.Year, src => src.MapFrom(s => s.Car.Year))
                .ForPath(c => c.Car.Price, src => src.MapFrom(s => s.Car.Price))
                .ForPath(c => c.Car.Color, src => src.MapFrom(s => s.Car.Color))
                .ForPath(c => c.Car.Brand, src => src.MapFrom(s => s.Car.Brand))
                .ForPath(c => c.Car.IsAccident, src => src.MapFrom(s => s.Car.IsAccident))
                .ForPath(c => c.Dealer.FirstName, src => src.MapFrom(s => s.Car.User.FirstName))
                .ForPath(c => c.Dealer.LastName, src => src.MapFrom(s => s.Car.User.LastName))
                .ForPath(c => c.Dealer.City, src => src.MapFrom(s => s.Car.User.City))
                .ForPath(c => c.Dealer.Email, src => src.MapFrom(s => s.Car.User.Email))
                .ForPath(c => c.Dealer.PhoneNumber, src => src.MapFrom(s => s.Car.User.PhoneNumber));

            CreateMap<RentEntity, RentResponseModel>();
            CreateMap<UserEntity, RentMemberResponseModel>();

            CreateMap<CarEntity, GetDealerCarsResponseModel>()
                .ForPath(c => c.Car.Id, src => src.MapFrom(s => s.Id))
                .ForPath(c => c.Car.Year, src => src.MapFrom(s => s.Year))
                .ForPath(c => c.Car.Price, src => src.MapFrom(s => s.Price))
                .ForPath(c => c.Car.Color, src => src.MapFrom(s => s.Color))
                .ForPath(c => c.Car.Brand, src => src.MapFrom(s => s.Brand))
                .ForPath(c => c.Car.IsAccident, src => src.MapFrom(s => s.IsAccident))
                .ForPath(c => c.Car.Access, src => src.MapFrom(s => s.Access))
                .ForPath(c => c.Rents, src => src.MapFrom(s => s.Rents));         
        }
    }
}
