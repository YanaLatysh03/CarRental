using AutoMapper;
using Database.Entities;
using Rent.Models.Request;
using Rent.Models.Response;
using Rent.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rent.Services
{
    public class RentService : IRentService
    {
        public readonly IRentRepository _rentRepository;
        private readonly IMapper _mapper;

        public RentService(IRentRepository rentRepository, IMapper mapper)
        {
            _rentRepository = rentRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddRent(AddRentRequestModel rent, UserEntity renter)
        {
            var result = await _rentRepository.AddRent(rent, renter);

            return result;
        }

        public async Task<IList<GetRentsResponseModel>> GetRentCars(Guid renterId)
        {
            var result = await _rentRepository.GetRentCars(renterId);

            var rents = _mapper.Map<List<GetRentsResponseModel>>(result);

            return rents;
        }

        public async Task<bool> ChangeRentStatus(Guid rentId, RentStatus status)
        {
            var result = await _rentRepository.ChangeRentStatus(rentId, status);

            return result;
        }
    }
}
