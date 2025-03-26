
using Domain.DatabaseModels;
using Domain.DataTransfareObject;
using Domain.Models;

namespace ClientSide.Services.InterFace
{
    public interface IImageService
    {
        public Task<ServiceReturnModel<List<PreviewImageModel>>> GetImages();
        public Task<ServiceReturnModel<bool>> RemoveImage(int id);
        public Task<ServiceReturnModel<bool>> AddImage(CreatePreviewImageDto req);
        public Task<ServiceReturnModel<bool>> AddVideo(MultipartFormDataContent content);
        public Task<ServiceReturnModel<List<PreviewImageModel>>> GetVideos();
        public Task<ServiceReturnModel<bool>> DeleteVideo(int id);

        public Task<ServiceReturnModel<bool>> EditImage(UpdatePreviewImageDto req);

    }
}
