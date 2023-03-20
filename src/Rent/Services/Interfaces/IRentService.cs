using Database.Entities;
using Rent.Models.Request;
using Rent.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rent.Services.Interfaces
{
    public interface IRentService
    {
        Task<bool> AddRent(AddRentRequestModel rent, UserEntity renter);

        Task<IList<GetRentsResponseModel>> GetRentCars(Guid renterId);

        Task<bool> ChangeRentStatus(Guid rentId, RentStatus status);

    }
}
