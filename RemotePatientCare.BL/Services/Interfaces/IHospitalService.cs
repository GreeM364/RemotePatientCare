using RemotePatientCare.BLL.DataTransferObjects;

namespace RemotePatientCare.BLL.Services.Interfaces
{
    public interface IHospitalService
    {
        public Task<HospitalDTO> GetByIdAsync(string id);
        public Task<List<HospitalDTO>> GetAsync();
        public Task<HospitalDTO> CreateAsync(HospitalCreateDTO request);
        public Task<HospitalDTO> UpdateAsync(string id, HospitalUpdateDTO request);
        public Task DeleteAsync(string id);
        public Task<List<DoctorDTO>> GetDoctorsAsync(string id);
        public Task<List<PatientDTO>> GetPatientsAsync(string id);
        public Task<List<HospitalAdministratorDTO>> GetAdministratorsAsync(string id);
    }
}

