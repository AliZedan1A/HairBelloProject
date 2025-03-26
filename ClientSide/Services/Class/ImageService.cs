using ClientSide.Services.InterFace;
using Domain.DatabaseModels;
using Domain.DataTransfareObject;
using Domain.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ClientSide.Services.Class
{
    public class ImageService : IImageService
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
        public ImageService(IHttpClientFactory httpClientFactory)
        {
            _httpclient = httpClientFactory;
        }
        public async Task<ServiceReturnModel<bool>> AddImage(CreatePreviewImageDto req)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };


            string jsonData = JsonSerializer.Serialize(req.File, options);
            var client = _httpclient.CreateClient("Server");
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("PreviewImage", req.File);
            return await HandleResponse<bool>(response);
        }

        public async Task<ServiceReturnModel<bool>> EditImage(UpdatePreviewImageDto req)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };


            string jsonData = JsonSerializer.Serialize(req.File, options);
            var client = _httpclient.CreateClient("Server");
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"PreviewImage/{req.Id}", req.File);
            return await HandleResponse<bool>(response);
        }

        public async Task<ServiceReturnModel<List<PreviewImageModel>>> GetImages()
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.GetAsync("PreviewImage");
            var handle = await HandleResponse<List<PreviewImageModel>>(response);
            return handle;
        }

        public async Task<ServiceReturnModel<bool>> RemoveImage(int id)
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.DeleteAsync($"PreviewImage/{id}");
            return await HandleResponse<bool>(response);
        }

        public async Task<ServiceReturnModel<bool>> AddVideo(MultipartFormDataContent content)
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.PostAsync("PreviewImage/UploadVideo", content);
            return await HandleResponse<bool>(response);
        }

        public async Task<ServiceReturnModel<List<PreviewImageModel>>> GetVideos()
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.GetAsync("PreviewImage/GetVideos");
            var handle = await HandleResponse<List<PreviewImageModel>>(response);
            return handle;
        }

        public async Task<ServiceReturnModel<bool>> DeleteVideo(int id)
        {
            var client = _httpclient.CreateClient("Server");
            var response = await client.DeleteAsync($"PreviewImage/DeleteVideo/{id}");
            return await HandleResponse<bool>(response);
        }
    }
}
