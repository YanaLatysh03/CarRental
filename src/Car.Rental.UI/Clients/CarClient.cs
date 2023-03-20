using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using Car.Models.Response;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.IO;
using Car.Rental.UI.Models.Car;

namespace Car.Rental.UI.Clients
{
    public class CarClient
    {
        public CarClient()
        {
        }

        public async Task<CreatePaginationResponseModel> GetCarCatalogue(int? page)
        {
            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(30);

                var result = await client.GetAsync($"http://localhost:53068/car/catalogue?page={page}");

                var content = await result.Content.ReadAsAsync<CreatePaginationResponseModel>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> AddCar(AddCarRequestModel carModel, Guid dealerId)
        {
            try
            {
                using var ms = new MemoryStream();
                carModel.Image.CopyTo(ms);

                var fileBytes = ms.ToArray();

                var carModelWithByte = new AddCarWithByteImageRequestModel()
                {
                    DealerId = dealerId,
                    Year = carModel.Year,
                    Price = carModel.Price,
                    Color = carModel.Color,
                    Brand = carModel.Brand,
                    IsAccident = carModel.IsAccident,
                    Image = fileBytes
                };

                using var client = new HttpClient();

                client.Timeout = TimeSpan.FromMinutes(30);

                var result = await client.PostAsJsonAsync("http://localhost:53068/car/add-car", carModelWithByte);

                var content = await result.Content.ReadAsAsync<bool>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<GetCarCatalogueResponseModel> GetCarById(Guid carId)
        {
            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(30);

                var result = await client.GetAsync($"http://localhost:53068/car/by-Id?carId={carId}");

                var content = await result.Content.ReadAsAsync<GetCarCatalogueResponseModel>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CreatePaginationResponseModel> GetCarsByFilter(Car.Models.Request.ApplyFilterRequestModel filters)
        {
            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(30);

                var result = await client.PostAsJsonAsync($"http://localhost:53068/car/by-filter", filters);

                var content = await result.Content.ReadAsAsync<CreatePaginationResponseModel>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetDealerCarsResponseModel>> GetDealerCars(Guid dealerId)
        {
            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(30);

                var result = await client.GetAsync($"http://localhost:53068/car/dealer-cars?dealerId={dealerId}");

                var content = await result.Content.ReadAsAsync<List<GetDealerCarsResponseModel>>();

                return content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
