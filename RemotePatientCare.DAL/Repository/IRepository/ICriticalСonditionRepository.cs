using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.DAL.Repository.IRepository
{
    public interface ICriticalСonditionRepository : IRepository<CriticalСondition>
    {
        Task CreateAsync(CriticalСondition entity);
        Task<CriticalСondition> UpdateAsync(CriticalСondition entity);
    }
}
