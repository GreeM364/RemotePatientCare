using RemotePatientCare.DAL.Data;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.DAL.Repository
{
    public class PhysicalConditionRepository : Repository<PhysicalCondition>, IPhysicalConditionRepository
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
    }
}
