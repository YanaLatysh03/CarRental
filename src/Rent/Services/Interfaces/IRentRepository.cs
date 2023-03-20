using Database.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Rent.Models.Request;

namespace Rent.Services.Interfaces
{
    public interface IRentRepository
    {
        Task<bool> AddRent(AddRentRequestModel rent, UserEntity renter);

        Task<IList<RentEntity>> GetRentCars(Guid renterId);

        Task<bool> ChangeRentStatus(Guid rentId, RentStatus status);
    }
}
