using RemotePatientCare.BLL.DataTransferObjects;

namespace RemotePatientCare.BLL.Services.Interfaces
{
    public interface IPatientService
    {
        Task<PatientDTO> GetByIdAsync(string id);
        Task<List<PatientDTO>> GetAsync();
        Task<PatientDTO> CreateAsync(PatientCreateDTO request);
        Task<PatientDTO> UpdateAsync(string id, PatientUpdateDTO request);
        Task DeleteAsync(string id);
    }
}
