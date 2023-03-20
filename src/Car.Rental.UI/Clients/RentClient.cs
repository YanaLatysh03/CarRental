using Rent.Models.Request;
using System.Threading.Tasks;
using System;
using Database.Entities;
using System.Net.Http;
using System.Collections.Generic;
using Rent.Models.Response;

namespace Car.Rental.UI.Clients
{
    public class RentClient
    {

        public RentClient()
        {
        }

        public async Task<bool> AddRent(AddRentRequestModel rent, UserEntity renter)
        {
            try
            {
                rent.Renter = renter;

                using var client = new HttpClient();

                var result = await client.PostAsJsonAsync($"http://localhost:6522/rents/add-rent", rent);

                var content = await result.Content.ReadAsAsync<bool>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetRentsResponseModel>> GetRentCars(Guid renterId)
        {
            try
            {
                using var client = new HttpClient();

                var result = await client.GetAsync($"http://localhost:6522/rents/rented-cars?renterId={renterId}");

                var content = await result.Content.ReadAsAsync<List<GetRentsResponseModel>>();

                return content;
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
                using var client = new HttpClient();

                var result = await client.GetAsync($"http://localhost:6522/rents/change-status?rentId={rentId}&status={status}");

                var content = await result.Content.ReadAsAsync<bool>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
