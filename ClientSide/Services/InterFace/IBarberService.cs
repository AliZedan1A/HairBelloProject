using Domain.DatabaseModels;
using Domain.DataTransfareObject;
using Domain.Models;

namespace ClientSide.Services.InterFace
{
    public interface IBarberService
    {
        public Task<ServiceReturnModel<List<BarberModel>>> GetBarberListAsync();
        public Task<ServiceReturnModel<bool>> CreateBarber(CreateBarberDto req);
        public Task<ServiceReturnModel<bool>> DeleteBarber(int id);
        public Task<ServiceReturnModel<bool>> EditBarber(UpdateBarberDto req);
        public Task<ServiceReturnModel<bool>> AddBarberService(CreateServiceDto req);
        public Task<ServiceReturnModel<bool>> EditBarberService(UpdateServiceDto req);
        public  Task<ServiceReturnModel<bool>> DeleteService(int id);
        public Task<ServiceReturnModel<ServiceModel>> GetServiceByID(int id);
        public Task<ServiceReturnModel<bool>> SetBarberBreak(CreateBarberBreak req);
        public Task<ServiceReturnModel<bool>> RemoveBarberBreak(RemoveBarberBreak req);

    }
}
