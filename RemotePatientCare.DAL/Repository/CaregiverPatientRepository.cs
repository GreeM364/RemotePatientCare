using RemotePatientCare.DAL.Data;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.DAL.Repository
{
    public class CaregiverPatientRepository : Repository<CaregiverPatient>, ICaregiverPatientRepository
    {
        private readonly RemotePatientCareDbContext _db;
        public CaregiverPatientRepository(RemotePatientCareDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task CreateAsync(CaregiverPatient entity)
        {
            entity.CreatedDate = DateTime.Now;
            //TODO entity.CreatedBy 

            _db.CaregiverPatients.Add(entity);
            _db.BaseUsers.Add(entity.User);

            await SaveAsync();
        }

        public async Task<CaregiverPatient> UpdateAsync(CaregiverPatient entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            //TODO entity.LastModifiedBy

            _db.CaregiverPatients.Update(entity);
            _db.BaseUsers.Update(entity.User);

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
