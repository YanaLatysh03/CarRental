using Car.Database;
using Car.Models.Request;
using Car.Services.Interfaces;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Car.Services.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationContext _dbContext;

        public CarRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(IList<CarEntity> cars, int count)> GetCarCatalogue(int currentPage, int pageSize)
        {
            try
            {
                var count = await _dbContext.Cars.CountAsync();
                var result = await _dbContext.Cars.Include(u => u.User).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

                if (result != null)
                {
                    return (cars: result, count: count);
                }

                throw new NullReferenceException("Cars in catalogue is not found.");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> GetCarsCountByFilter(ApplyFilterRequestModel appliedFilters)
        {
            try
            {
                var cars = await _dbContext.Cars.ToListAsync();

                cars = FilterData(appliedFilters, cars);

                var count = cars.Count;

                return count;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CarEntity> GetCarById(Guid carId)
        {
            try
            {
                var car = await _dbContext.Cars.Include(c => c.User).FirstOrDefaultAsync(u => u.Id == carId);

                return car;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> AddCar(AddCarRequestModel carModel, byte[] image, Guid dealerId)
        {
            try
            {
                var car = new CarEntity()
                {
                    Year = carModel.Year,
                    Price = carModel.Price,
                    Color = carModel.Color,
                    Brand = carModel.Brand,
                    IsAccident = carModel.IsAccident,
                    Image = image,
                    UserId = dealerId
                };

                await _dbContext.AddAsync(car);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IList<CarEntity>> GetCarsByFilter(ApplyFilterRequestModel appliedFilters, int currentPage, int pageSize)
        {
            try
            {
                var result = await _dbContext.Cars.Include(u => u.User).ToListAsync();

                result = FilterData(appliedFilters, result);
                result = result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IList<CarEntity>> GetDealerCars(Guid dealerId)
        {
            try
            {
                var cars = await _dbContext.Cars.Where(c => c.UserId == dealerId).Include(c => c.Rents).ThenInclude(r => r.Users.Where(u => u.Id != dealerId)).ToListAsync();

                return cars;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private List<CarEntity> FilterData(ApplyFilterRequestModel model, List<CarEntity> data)
        {
            data = data.Where(d => model.Brand == "All" || d.Brand == model.Brand)
                       .Where(d => model.Color == "All" || d.Color == model.Color.ToLower())
                       .Where(d => model.Access == "All" || d.Access.ToString() == model.Access)
                       .Where(d => model.YearFrom == null || d.Year >= model.YearFrom)
                       .Where(d => model.YearTo == null || d.Year <= model.YearTo)
                       .Where(d => model.PriceFrom == null || d.Price >= model.PriceFrom)
                       .Where(d => model.PriceTo == null || d.Price <= model.PriceTo)
                .ToList();

            return data;
        }
    }
}
