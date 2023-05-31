using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.DAL.Repository.IRepository
{
    public interface IHospitalRepository : IRepository<Hospital>
    {
        Task CreateAsync(Hospital entity);
    }
}
