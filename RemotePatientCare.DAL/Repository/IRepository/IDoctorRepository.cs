using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.DAL.Repository.IRepository
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task CreateAsync(Doctor entity, string password);
    }
}
