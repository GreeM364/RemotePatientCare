using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.DAL.Repository.IRepository
{
    public interface IPhysicalConditioneRepository : IRepository<PhysicalCondition>
    {
        Task CreateAsync(PhysicalCondition entity);
        Task<PhysicalCondition> UpdateAsync(PhysicalCondition entity);
    }
}
