using Domain.Models;
using Microsoft.EntityFrameworkCore;
using ServerSide.Database;


namespace ServerSide.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceReturnModel<List<T>>> GetAllAsync()
        {
            var result = new ServiceReturnModel<List<T>>();
            try
            {
                result.Value = await _context.Set<T>().ToListAsync();
                result.IsSucceeded = true;
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Comment = "Error fetching data: " + ex.Message;
            }
            return result;
        }

        public async Task<ServiceReturnModel<T>> GetAsync(int id)
        {
            var result = new ServiceReturnModel<T>();
            try
            {
                result.Value = await _context.Set<T>().FindAsync(id);
                result.IsSucceeded = result.Value != null;
                if (!result.IsSucceeded)
                    result.Comment = "Entity not found";
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Comment = "Error fetching entity: " + ex.Message;
            }
            return result;
        }

        public async Task<ServiceReturnModel<bool>> InsertAsync(T entity)
        {
            var result = new ServiceReturnModel<bool>();
            if (entity == null)
            {
                result.IsSucceeded = false;
                result.Comment = "Entity is null";
                return result;
            }

            try
            {
                await _context.Set<T>().AddAsync(entity);
                await SaveChangesAsync();
                result.IsSucceeded = true;
                result.Value = true;
                result.Comment = "Entity inserted successfully";
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Comment = "Error inserting entity: " + ex.Message;
            }
            return result;
        }

        public async Task<ServiceReturnModel<bool>> UpdateAsync(T entity)
        {
            var result = new ServiceReturnModel<bool>();
            if (entity == null)
            {
                result.IsSucceeded = false;
                result.Comment = "Entity is null";
                return result;
            }

            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await SaveChangesAsync();
                result.IsSucceeded = true;
                result.Value = true;
                result.Comment = "Entity updated successfully";
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Comment = "Error updating entity: " + ex.Message;
            }
            return result;
        }

        public async Task<ServiceReturnModel<bool>> DeleteAsync(int id)
        {
            var result = new ServiceReturnModel<bool>();
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity == null)
                {
                    result.IsSucceeded = false;
                    result.Comment = "Entity not found";
                    return result;
                }

                _context.Set<T>().Remove(entity);
                await SaveChangesAsync();
                result.IsSucceeded = true;
                result.Value = true;
                result.Comment = "Entity deleted successfully";
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Comment = "Error deleting entity: " + ex.Message;
            }
            return result;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}