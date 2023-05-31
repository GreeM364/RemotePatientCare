using RemotePatientCare.DAL.Models;
using System.Linq.Expressions;

namespace RemotePatientCare.DAL.Repository.IRepository
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, 
                                  IOrderedQueryable<T>>? orderBy = null, string includeProperties = null, 
                                  bool isTracking = true);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, string includeProperties = null, bool isTracking = true);

        Task<T> GetByIdAsync(string id);
        Task<T> UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entity);
        Task SaveAsync();
    }
}
