using RemotePatientCare.DAL.Data;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.DAL.Repository
{
    public class HospitalAdministratorRepository : Repository<HospitalAdministrator>, IHospitalAdministratorRepository
    {
        private readonly RemotePatientCareDbContext _db;
        public HospitalAdministratorRepository(RemotePatientCareDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task CreateAsync(HospitalAdministrator entity)
        {
            entity.CreatedDate = DateTime.Now;
            //TODO entity.CreatedBy 

            _db.HospitalAdministrators.Add(entity);
            _db.BaseUsers.Add(entity.User);

            await SaveAsync();
        }

        public async Task<HospitalAdministrator> UpdateAsync(HospitalAdministrator entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            //TODO entity.LastModifiedBy

            _db.HospitalAdministrators.Update(entity);
            _db.BaseUsers.Update(entity.User);

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
