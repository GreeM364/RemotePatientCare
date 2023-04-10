using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.DAL.Repository.IRepository
{
    public interface ICaregiverPatientRepository : IRepository<CaregiverPatient>
    {
        Task CreateAsync(CaregiverPatient entity);
        Task<CaregiverPatient> UpdateAsync(CaregiverPatient entity);
    }
}
