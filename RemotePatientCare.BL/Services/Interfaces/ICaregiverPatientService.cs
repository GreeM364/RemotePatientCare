using RemotePatientCare.BLL.DataTransferObjects;

namespace RemotePatientCare.BLL.Services.Interfaces
{
    public interface ICaregiverPatientService
    {
        Task<CaregiverPatientDTO> GetByIdAsync(string id);
        Task<List<CaregiverPatientDTO>> GetAsync();
        Task<CaregiverPatientDTO> CreateAsync(CaregiverPatientCreateDTO request);
        Task<CaregiverPatientDTO> UpdateAsync(string id, CaregiverPatientUpdateDTO request);
        Task DeleteAsync(string id);
        Task<List<PatientDTO>> GetPatientsAsync(string id);
    }
}
