using Domain.Models;

namespace ServerSide.Repositories
{
    public interface IRepository <T>
    {
        Task<ServiceReturnModel<List<T>>>? GetAllAsync();
        Task<ServiceReturnModel<T>>? GetAsync(int id);
        Task<ServiceReturnModel<bool>> UpdateAsync(T entity);
        Task<ServiceReturnModel<bool>> DeleteAsync(int id);
        Task<ServiceReturnModel<bool>> InsertAsync(T entity);
        Task SaveChangesAsync();
    }
}
