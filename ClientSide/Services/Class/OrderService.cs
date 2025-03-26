using ClientSide.Services.InterFace;
using Domain.DataTransfareObject;
using Domain.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using Domain.DatabaseModels;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;

namespace ClientSide.Services.Class
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory _httpclient;

        public OrderService(IHttpClientFactory httpClientFactory)
        {
            _httpclient = httpClientFactory;    
        }
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
        public async Task<ServiceReturnModel<List<OrderModel>>> GetPastOrders(DateTime from,DateTime to)
        {
            var client = _httpclient.CreateClient("Server");
            var FromStr = from.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var toStr = to.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            var response = await client.GetAsync($"Orders/GetPastOrders?from={FromStr}&to={toStr}");
            return await HandleResponse<List<OrderModel>>(response);

        }
        public async Task<ServiceReturnModel<bool>> SetOrderAsync(CreateOrderDto order)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string jsonData = JsonSerializer.Serialize(order, options);
            var client = _httpclient.CreateClient("Server");
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Orders", jsonContent);
            return await HandleResponse<bool>(response);
        }

        public async Task<ServiceReturnModel<List<OrderModel>>> GetOrdersLimited(GetElementsLimtedDto req)
        {
            var client = _httpclient.CreateClient("Server");
            var formattedDate = req.date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var response = await client.GetAsync($"Orders/GetOrderLimited?Start={req.Start}&End={req.End}&BarberId={req.BarberId}&date={formattedDate}");
            return await HandleResponse< List <OrderModel>> (response);

        }

        public Task<ServiceReturnModel<List<OrderModel>>> GetOrdersSortedByBarber(GetElementsLimtedSortedByDay req)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceReturnModel<bool>> DeleteOrder(int id)
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.DeleteAsync($"Orders/{id}");
            return await HandleResponse<bool>(response);
        }

        public async Task<ServiceReturnModel<OrderModel>> GetLastOrder(string PhonNumber)
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.GetAsync($"Orders/GetLastOrder/{PhonNumber}");
            return await HandleResponse<OrderModel>(response);
        }

        public async Task<ServiceReturnModel<bool>> CanselOrder(int OrderId)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string jsonData = JsonSerializer.Serialize(OrderId, options);
            var client = _httpclient.CreateClient("Server");
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("Orders/CancelOrder", jsonContent);
            return await HandleResponse<bool>(response);
        }
    }
}