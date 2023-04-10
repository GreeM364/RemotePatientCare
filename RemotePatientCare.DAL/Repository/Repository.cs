using Microsoft.EntityFrameworkCore;
using RemotePatientCare.DAL.Data;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;
using System.Linq.Expressions;

namespace RemotePatientCare.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly RemotePatientCareDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(RemotePatientCareDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null, 
                                      bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (!isTracking)
                query = query.AsNoTracking();

            return query.FirstOrDefault();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, 
                                               IOrderedQueryable<T>>? orderBy = null, string includeProperties = null, 
                                               bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
                query = orderBy(query);

            if (!isTracking)
                query = query.AsNoTracking();

            return query.ToList();
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}

