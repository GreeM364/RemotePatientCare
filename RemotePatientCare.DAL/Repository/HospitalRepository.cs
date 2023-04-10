using RemotePatientCare.DAL.Data;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.DAL.Repository
{
    public class HospitalRepository : Repository<Hospital>, IHospitalRepository
    {
        private readonly RemotePatientCareDbContext _db;
        public HospitalRepository(RemotePatientCareDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task CreateAsync(Hospital entity)
        {
            entity.CreatedDate = DateTime.Now;
            //TODO entity.CreatedBy 

            _db.Hospitals.Add(entity);

            await SaveAsync();
        }

        public async Task<Hospital> UpdateAsync(Hospital entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            //TODO entity.LastModifiedBy

            _db.Hospitals.Update(entity);

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
