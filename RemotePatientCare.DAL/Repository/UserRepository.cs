using Microsoft.EntityFrameworkCore;
using RemotePatientCare.DAL.Data;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly RemotePatientCareDbContext _db;

        public UserRepository(RemotePatientCareDbContext db)
        {
            _db = db;
        }

        public async Task<User> GetAsync(string id, string includeProperties = null)
        {
            IQueryable<User> query = _db.BaseUsers;

            query = query.Where(x => x.Id == id);

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _db.BaseUsers.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
