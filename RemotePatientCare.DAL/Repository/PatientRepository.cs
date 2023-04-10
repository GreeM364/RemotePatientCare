using RemotePatientCare.DAL.Data;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.DAL.Repository
{
    internal class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private readonly RemotePatientCareDbContext _db;
        public PatientRepository(RemotePatientCareDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task CreateAsync(Patient entity)
        {
            entity.CreatedDate = DateTime.Now;
            //TODO entity.CreatedBy 

            _db.Patients.Add(entity);
            _db.BaseUsers.Add(entity.User);

            await SaveAsync();
        }

        public async Task<Patient> UpdateAsync(Patient entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            //TODO entity.LastModifiedBy

            _db.Patients.Update(entity);
            _db.BaseUsers.Update(entity.User);

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
