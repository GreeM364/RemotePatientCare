using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.DAL.Repository.IRepository
{
    public interface IHospitalAdministratorRepository : IRepository<HospitalAdministrator>
    {
        Task CreateAsync(HospitalAdministrator entity, string password);
        Task<HospitalAdministrator> UpdateAsync(HospitalAdministrator entity);
    }
}
