using Domain.DatabaseModels;
using Domain.DataTransfareObject;
using Domain.Models;

namespace ClientSide.Services.InterFace
{
    public interface IBarberSchedule
    {
        public Task<ServiceReturnModel<List<AvailableSlotModel>>> GetScheduleByDay(int BarberId, string day, string ServiceIds, DateTime date);

        public Task<ServiceReturnModel<bool>> AddScheduleByDay(CreateBarberScheduleDto req);

        public Task<ServiceReturnModel<bool>> RemoveScheduleByDay(int id);

        public Task<ServiceReturnModel<List<BarberScheduleModel>>> GetBarberSchedule(int id);

        public Task<ServiceReturnModel<bool>> EditScheduleById(UpdateBarberScheduleDto req);

    }
}
