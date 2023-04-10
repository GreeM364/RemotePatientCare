using RemotePatientCare.DAL.Data;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.DAL.Repository
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly RemotePatientCareDbContext _db;
        public DoctorRepository(RemotePatientCareDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task CreateAsync(Doctor entity)
        {
            entity.CreatedDate = DateTime.Now;
            //TODO entity.CreatedBy 

            _db.Doctors.Add(entity);
            _db.BaseUsers.Add(entity.User);

            await SaveAsync();
        }

        public async Task<Doctor> UpdateAsync(Doctor entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            //TODO entity.LastModifiedBy

            _db.Doctors.Update(entity);
            _db.BaseUsers.Update(entity.User);

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
