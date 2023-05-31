using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.DAL.Repository.IRepository
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task CreateAsync(Patient entity, string password);
    }
}
