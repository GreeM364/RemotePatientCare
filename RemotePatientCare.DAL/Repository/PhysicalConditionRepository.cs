using RemotePatientCare.DAL.Data;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.DAL.Repository
{
    public class PhysicalConditionRepository : Repository<PhysicalCondition>, IPhysicalConditioneRepository
    {
        private readonly RemotePatientCareDbContext _db;
        public PhysicalConditionRepository(RemotePatientCareDbContext db) : base(db)
        {
            _db = db;   
        }

        public async Task CreateAsync(PhysicalCondition entity)
        {
            entity.CreatedDate = DateTime.Now;

            _db.PhysicalConditions.Add(entity);

            await SaveAsync();
        }

        public async Task<PhysicalCondition> UpdateAsync(PhysicalCondition entity)
        {
            entity.LastModifiedDate = DateTime.Now;

            _db.PhysicalConditions.Update(entity);

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
