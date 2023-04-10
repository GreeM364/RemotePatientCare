using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.DAL.Repository.IRepository
{
    public interface IHospitalAdministratorRepository : IRepository<HospitalAdministrator>
    {
        Task CreateAsync(HospitalAdministrator entity);
        Task<HospitalAdministrator> UpdateAsync(HospitalAdministrator entity);
    }
}
