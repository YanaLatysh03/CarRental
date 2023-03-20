using AutoMapper;
using Car.Models.Request;
using Car.Models.Response;
using Car.Services.Interfaces;
using Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Car.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        private const int PageSize = 4;

        public CarService(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<CreatePaginationResponseModel> GetCarCatalogue(int? page)
        {
            var currentPage = page != null ? (int)page : 1;

            var result = await _carRepository.GetCarCatalogue(currentPage, PageSize);

            var pages = new Pages(result.count, currentPage, PageSize);

            var cars = _mapper.Map<List<GetCarCatalogueResponseModel>>(result.cars);

            var carModel = new CreatePaginationResponseModel()
            {
                Cars = cars,
                Pages = pages,
                IsApplyFilter = false
            };

            return carModel;
        }

        public async Task<bool> AddCar(AddCarRequestModel carModel, Guid dealerId)
        {
            var result = await _carRepository.AddCar(carModel, carModel.Image, dealerId);

            return result;
        }

        public async Task<GetCarCatalogueResponseModel> GetCarById(Guid carId)
        {
            var result = await _carRepository.GetCarById(carId);

            var car = _mapper.Map<GetCarCatalogueResponseModel>(result);

            return car;
        }

        public async Task<CreatePaginationResponseModel> GetCarsByFilter(ApplyFilterRequestModel model)
        {
            var currentPage = model.Page != null ? (int)model.Page : 1;

            var carsCount = await _carRepository.GetCarsCountByFilter(model);

            var pages = new Pages(carsCount, currentPage, PageSize);

            var cars = await _carRepository.GetCarsByFilter(model, pages.CurrentPage, pages.PageSize);

            var result = _mapper.Map<List<GetCarCatalogueResponseModel>>(cars);

            var carModel = new CreatePaginationResponseModel()
            {
                Cars = result,
                Pages = pages,
                IsApplyFilter = true
            };

            return carModel;
        }

        public async Task<List<GetDealerCarsResponseModel>> GetDealerCars(Guid dealerId)
        {
            var rentedResult = await _carRepository.GetDealerCars(dealerId);
            var rentedCars = _mapper.Map<List<GetDealerCarsResponseModel>>(rentedResult);

            return rentedCars;
        }
    }
}
