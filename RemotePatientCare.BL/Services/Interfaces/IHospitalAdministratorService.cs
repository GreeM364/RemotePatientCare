using RemotePatientCare.BLL.DataTransferObjects;

namespace RemotePatientCare.BLL.Services.Interfaces
{
    public interface IHospitalAdministratorService
    {
        public Task<HospitalAdministratorDTO> GetByIdAsync(string id);
        public Task<List<HospitalAdministratorDTO>> GetAsync();
        public Task<HospitalAdministratorDTO> CreateAsync(HospitalAdministratorCreateDTO request);
        public Task<HospitalAdministratorDTO> UpdateAsync(string id, HospitalAdministratorUpdateDTO request);
        public Task DeleteAsync(string id);
    }
}
