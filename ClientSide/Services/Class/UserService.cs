

using ClientSide.Services.InterFace;
using Domain.DatabaseModels;
using Domain.Models;
using Microsoft.Maui.Controls;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using Domain.DataTransfareObject;

namespace ClientSide.Services.Class
{
    class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpclient;
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
        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpclient = httpClientFactory;
        }

        public Task<ServiceReturnModel<List<UserModel>>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceReturnModel<bool>> ToogleBan(ToogleBanDto PhonNumber)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string jsonData = JsonSerializer.Serialize(PhonNumber, options);
            var client = _httpclient.CreateClient("Server");
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Users/ToogleBan", jsonContent);
            return await HandleResponse<bool>(response);
        }

        public async Task<ServiceReturnModel<UserModel>> GetUserByPhonNumber(string PhonNumber)
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.GetAsync($"Users/GetUserByPhonNumber?PhonNumber={PhonNumber}");
            return await HandleResponse<UserModel>(response);

        }

        public async Task<ServiceReturnModel<bool>> ToogleApp(ToogleAppDto req)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string jsonData = JsonSerializer.Serialize(req, options);
            var client = _httpclient.CreateClient("Server");
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Users/ToogleApp", jsonContent);
            return await HandleResponse<bool>(response);
        }

        public async Task<ServiceReturnModel<bool>> InsertUser(CreateUserDto req)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string jsonData = JsonSerializer.Serialize(req, options);
            var client = _httpclient.CreateClient("Server");
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Users", jsonContent);
            return await HandleResponse<bool>(response);
        }

        public async Task<ServiceReturnModel<AppConfigModel>> GetConfig()
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.GetAsync($"Users/GetAppStatus");
            return await HandleResponse<AppConfigModel>(response);
        }

        public async Task<ServiceReturnModel<bool>> CheckCode(string PhonNumber, string code)
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.GetAsync($"Users/IsCodeAvailble?PhonNumber={PhonNumber}&Code={code}");
            return await HandleResponse<bool>(response);
        }

        public async Task<ServiceReturnModel<bool>> SendValidtion(string PhonNumber, string UserName)
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.GetAsync($"Users/SendVerfy?PhonNumber={PhonNumber}&UserName={UserName}");
            return await HandleResponse<bool>(response);
        }
    }
}
