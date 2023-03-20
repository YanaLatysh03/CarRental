using Car.Models.Request;
using Car.Models.Response;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car.Services.Interfaces
{
    public interface ICarRepository
    {
        Task<(IList<CarEntity> cars, int count)> GetCarCatalogue(int currentPage, int pageSize);

        Task<CarEntity> GetCarById(Guid carId);

        Task<bool> AddCar(AddCarRequestModel carModel, byte[] image, Guid userId);

        Task<int> GetCarsCountByFilter(ApplyFilterRequestModel appliedFilters);

        Task<IList<CarEntity>> GetCarsByFilter(ApplyFilterRequestModel appliedFilters, int currentPage, int pageSize);

        Task<IList<CarEntity>> GetDealerCars(Guid dealerId);
    }
}
