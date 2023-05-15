using RemotePatientCare.BLL.DataTransferObjects;

namespace RemotePatientCare.BLL.Services.Interfaces
{
    public interface ICriticalСonditionService
    {
        public Task<CriticalСonditionDTO> GetByIdAsync(string id);
        public Task<List<CriticalСonditionDTO>> GetAsync();
        public Task CreateAsync(CriticalСonditionCreateDTO request);
        public Task<List<CriticalСonditionDTO>> GetCriticalСonditionByPatientAsync(string id);
    }
}

