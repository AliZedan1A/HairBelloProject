using ClientSide.Services.InterFace;
using Domain.DatabaseModels;
using Domain.DataTransfareObject;
using Domain.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace ClientSide.Services.Class
{
    public class BarberService : IBarberService
    {
        private async Task<ServiceReturnModel<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            try
            {
                var model = await response.Content.ReadFromJsonAsync<ServiceReturnModel<T>>();
                if (model is null)
                    return new() { IsSucceeded = false, Comment = "The response is null" };
                return model;
            }
            catch (Exception ex)
            {
                return new() { IsSucceeded = false, Comment = ex.Message };
            }
        }
        private readonly IHttpClientFactory _httpclient;
        public BarberService(IHttpClientFactory httpClientFactory)
        {
            _httpclient = httpClientFactory;
        }
        public async Task<ServiceReturnModel<List<BarberModel>>> GetBarberListAsync()
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.GetAsync("Barber");
            var handle = await HandleResponse<List<BarberModel>>(response);
            return handle;

        }
        public async Task<ServiceReturnModel<bool>> EditBarberService(UpdateServiceDto req)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };


            string jsonData = JsonSerializer.Serialize(req, options);
            var client = _httpclient.CreateClient("Server");
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("Services", jsonContent);
            return await HandleResponse<bool>(response);
        }

        public async Task<ServiceReturnModel<bool>> AddBarberService(CreateServiceDto req)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };


            string jsonData = JsonSerializer.Serialize(req, options);
            var client = _httpclient.CreateClient("Server");
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Services", jsonContent);
            return await HandleResponse<bool>(response);

        }
        public async Task<ServiceReturnModel<bool>> CreateBarber(CreateBarberDto req)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };


            string jsonData = JsonSerializer.Serialize(req, options);
            var client = _httpclient.CreateClient("Server");
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Barber", jsonContent);
            return await HandleResponse<bool>(response);

        }

        public async Task<ServiceReturnModel<bool>> DeleteBarber(int id)
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.DeleteAsync($"Barber/{id}");
            return await HandleResponse<bool>(response);


        }
        public async Task<ServiceReturnModel<bool>> DeleteService(int id)
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.DeleteAsync($"Services/{id}");
            return await HandleResponse<bool>(response);
        }

        public async Task<ServiceReturnModel<bool>> EditBarber(UpdateBarberDto req)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };


            string jsonData = JsonSerializer.Serialize(req, options);
            var client = _httpclient.CreateClient("Server");
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("Barber", jsonContent);
            return await HandleResponse<bool>(response);
        }

        public async Task<ServiceReturnModel<ServiceModel>> GetServiceByID(int id)
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.GetAsync($"Services/{id}");
            return await HandleResponse<ServiceModel>(response);
        }

        public async Task<ServiceReturnModel<bool>> SetBarberBreak(CreateBarberBreak req)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };


            string jsonData = JsonSerializer.Serialize(req, options);
            var client = _httpclient.CreateClient("Server");
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("Barber/SetBarberBreak", jsonContent);
            return await HandleResponse<bool>(response);
        }

        public async Task<ServiceReturnModel<bool>> RemoveBarberBreak(RemoveBarberBreak req)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };


            string jsonData = JsonSerializer.Serialize(req, options);
            var client = _httpclient.CreateClient("Server");
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("Barber/RemoveBarberBreak", jsonContent);
            return await HandleResponse<bool>(response);
        }
    }
}
