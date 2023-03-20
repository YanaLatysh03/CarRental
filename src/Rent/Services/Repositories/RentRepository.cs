using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Rent.Database;
using Rent.Models.Request;
using Rent.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rent.Services.Repositories
{
    public class RentRepository : IRentRepository
    {
        private readonly ApplicationContext _dbContext;

        public RentRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddRent(AddRentRequestModel rent, UserEntity renter)
        {
            try
            {
                var car = await _dbContext.Cars.FirstOrDefaultAsync(c => c.Id == rent.CarId);

                var rentModel = new RentEntity()
                {
                    Country = rent.Country,
                    City = rent.City,
                    Street = rent.Street,
                    House = rent.House,
                    Building = rent.Building,
                    Appartment = rent.Appartment,
                    IsAdvancePayment = true,
                    Currency = "USD",
                    CarId = car.Id,
                    DateFrom = rent.DateFrom,
                    DateTo = rent.DateTo,
                    Users = new List<UserEntity> { renter }
                };

                await _dbContext.AddAsync(rentModel);
                await _dbContext.SaveChangesAsync();

                car.Access = RentAccess.Rented;
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IList<RentEntity>> GetRentCars(Guid renterId)
        {
            try
            {
                var rents = await _dbContext.Rents.Include(r => r.Users).Include(r => r.Car).ThenInclude(t => t.User).Where(u => u.Users.Any(a => a.Id == renterId)).ToListAsync();

                return rents;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> ChangeRentStatus(Guid rentId, RentStatus status)
        {
            try
            {
                var rent = await _dbContext.Rents.Where(r => r.Id == rentId).FirstOrDefaultAsync();

                rent.Status = status;

                await _dbContext.SaveChangesAsync();

                var car = await _dbContext.Cars.Where(c => c.Id == rent.CarId).FirstOrDefaultAsync();

                if (status == RentStatus.Successful || status == RentStatus.Failed)
                {
                    car.Access = RentAccess.Free;
                    await _dbContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
