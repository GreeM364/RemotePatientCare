using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.DAL.Repository.IRepository
{
    public interface IPhysicalConditionRepository : IRepository<PhysicalCondition>
    {
        Task CreateAsync(PhysicalCondition entity);
        Task<PhysicalCondition> UpdateAsync(PhysicalCondition entity);
    }
}
