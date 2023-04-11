using RemotePatientCare.BLL.DataTransferObjects;

namespace RemotePatientCare.BLL.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<DoctorDTO> GetByIdAsync(string id);
        Task<List<DoctorDTO>> GetAsync();
        Task<DoctorDTO> CreateAsync(DoctorCreateDTO request);
        Task<DoctorDTO> UpdateAsync(string id, DoctorUpdateDTO request);
        Task DeleteAsync(string id);
    }
}
