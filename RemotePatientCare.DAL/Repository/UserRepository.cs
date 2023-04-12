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

        public async Task<User> GetByIdAsync(string id)
        {
            return await _db.BaseUsers.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
