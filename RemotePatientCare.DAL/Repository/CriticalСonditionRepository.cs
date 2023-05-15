using RemotePatientCare.DAL.Data;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;


namespace RemotePatientCare.DAL.Repository 
{
    public class CriticalСonditionRepository : Repository<CriticalСondition>, ICriticalСonditionRepository
    {
        private readonly RemotePatientCareDbContext _db;
        public CriticalСonditionRepository(RemotePatientCareDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task CreateAsync(CriticalСondition entity)
        {
            entity.CreatedDate = DateTime.Now;

            _db.CriticalСonditions.Add(entity);

            await SaveAsync();
        }

        public async Task<CriticalСondition> UpdateAsync(CriticalСondition entity)
        {
            entity.LastModifiedDate = DateTime.Now;

            _db.CriticalСonditions.Update(entity);

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
