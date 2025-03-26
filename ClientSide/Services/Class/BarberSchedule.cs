
using ClientSide.Services.InterFace;
using Domain.DatabaseModels;
using Domain.DataTransfareObject;
using Domain.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Globalization;

namespace ClientSide.Services.Class
{
    public class BarberSchedule : IBarberSchedule
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
        public BarberSchedule(IHttpClientFactory httpClientFactory)
        {
            _httpclient = httpClientFactory;
        }

        public async Task<ServiceReturnModel<List<AvailableSlotModel>>> GetScheduleByDay(int BarberId,string day,string ServiceIds,DateTime date)
        {
            var client = _httpclient.CreateClient("Server");
            var formattedDate = date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var response = await client.GetAsync($"BarberSchedule/GetAvailbleSolts?BarberId={BarberId}&ServiceIds={ServiceIds}&DayName={day}&Date={formattedDate}");
            var handle = await HandleResponse<List<AvailableSlotModel>>(response);
            return handle;
        }

        public async Task<ServiceReturnModel<bool>> AddScheduleByDay(CreateBarberScheduleDto req)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };


            string jsonData = JsonSerializer.Serialize(req, options);
            var client = _httpclient.CreateClient("Server");
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("BarberSchedule", jsonContent);
            return await HandleResponse<bool>(response);
        }

        public async Task<ServiceReturnModel<bool>> RemoveScheduleByDay(int id)
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.DeleteAsync($"BarberSchedule/{id}");
            return await HandleResponse<bool>(response);
        }

        public async Task<ServiceReturnModel<List<BarberScheduleModel>>> GetBarberSchedule(int id)
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.GetAsync("BarberSchedule");
            return await HandleResponse<List<BarberScheduleModel>>(response);
        }

        public async Task<ServiceReturnModel<bool>> EditScheduleById(UpdateBarberScheduleDto req)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };


            string jsonData = JsonSerializer.Serialize(req, options);
            var client = _httpclient.CreateClient("Server");
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("BarberSchedule", jsonContent);
            return await HandleResponse<bool>(response);
        }

        //public Task<ServiceReturnModel<List<AvailableSlotModel>>> GetScheduleByDay(int BarberId, string day, string ServiceIds, DateTime date)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
