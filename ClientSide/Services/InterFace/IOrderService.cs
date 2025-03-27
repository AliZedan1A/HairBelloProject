using Domain.DatabaseModels;
using Domain.DataTransfareObject;
using Domain.Models;

namespace ClientSide.Services.InterFace
{
    public interface IOrderService
    {
        public Task<ServiceReturnModel<bool>> SetOrderAsync(CreateOrderDto order);
        public Task<ServiceReturnModel<List<OrderModel>>> GetOrdersLimited(GetElementsLimtedDto req);
        public Task<ServiceReturnModel<List<OrderModel>>> GetOrdersSortedByBarber(GetElementsLimtedSortedByDay req);
        public Task<ServiceReturnModel<bool>> DeleteOrder(int id);
        public Task<ServiceReturnModel<OrderModel>> GetLastOrder(string PhonNumber);
        public Task<ServiceReturnModel<bool>> CanselOrder(int OrderId);
        public Task<ServiceReturnModel<bool>> AdminCanselOrder(AdminCansleOrderDto req);

        public Task<ServiceReturnModel<List<OrderModel>>> GetPastOrders(DateTime from, DateTime to);
    }
}
