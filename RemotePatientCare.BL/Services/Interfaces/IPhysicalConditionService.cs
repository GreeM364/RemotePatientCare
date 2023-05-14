using RemotePatientCare.BLL.DataTransferObjects;

namespace RemotePatientCare.BLL.Services.Interfaces
{
    public interface IPhysicalConditionService
    {
        public Task<PhysicalConditionDTO> GetByIdAsync(string id);
        public Task<List<PhysicalConditionDTO>> GetAsync();
        public Task CreateAsync(PhysicalConditionCreateDTO request);
        public Task<List<PhysicalConditionDTO>> GetPhysicalConditionByPatientAsync(string id);
    }
}
