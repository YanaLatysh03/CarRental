using Car.Models.Request;
using Car.Models.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Services.Interfaces
{
    public interface ICarService
    {
        Task<CreatePaginationResponseModel> GetCarCatalogue(int? page);

        Task<bool> AddCar(AddCarRequestModel carModel, Guid userId);

        Task<GetCarCatalogueResponseModel> GetCarById(Guid carId);

        Task<CreatePaginationResponseModel> GetCarsByFilter(ApplyFilterRequestModel model);

        Task<List<GetDealerCarsResponseModel>> GetDealerCars(Guid dealerId);
    }
}
