using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.BLL.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IHospitalAdministratorRepository _hospitalAdministratorRepository;
        private readonly IMapper _mapper;

        public HospitalService(IHospitalRepository hospitalRepository, IDoctorRepository doctorRepository,
                               IPatientRepository patientRepository, IHospitalAdministratorRepository hospitalAdministratorRepository,
                               IMapper mapper)
        {
            _hospitalRepository = hospitalRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _hospitalAdministratorRepository = hospitalAdministratorRepository;
            _mapper = mapper;
        }

        public async Task<HospitalDTO> GetByIdAsync(string id)
        {
            var source = await _hospitalRepository.GetByIdAsync(id);

            if (source == null)
            {
                throw new Exception($"Hospital with id \"{id}\" not found");
            }

            return _mapper.Map<HospitalDTO>(source);
        }

        public async Task<List<HospitalDTO>> GetAsync()
        {
            var source = await _hospitalRepository.GetAllAsync();
            return _mapper.Map<List<HospitalDTO>>(source); 
        }

        public async Task<HospitalDTO> CreateAsync(HospitalCreateDTO request)
        {
            var existing = await _hospitalRepository.GetAsync(x => x.Name == request.Name);

            if (existing != null)
            {
                throw new Exception($"Hospital with name {request.Name} already exists");
            }
            if (request == null)
            {
                throw new Exception("The request model of Hospital is null");
            }

            var createEntity = _mapper.Map<Hospital>(request);
            await _hospitalRepository.CreateAsync(createEntity);

            var result = _mapper.Map<HospitalDTO>(createEntity);
            return result;
        }

        public async Task<HospitalDTO> UpdateAsync(string id, HospitalUpdateDTO request)
        {
            if (request == null)
                throw new Exception("The received model of Hospital is null");

            if (await _hospitalRepository.GetAsync(x => x.Id == id) != null)
                throw new Exception($"Hospital with id \"{id}\" not found");

            if (await _hospitalRepository.GetAsync(x => x.Name == request.Name && x.Id != id) != null)
                throw new Exception("Hospital with such name already exists");

            if (await _hospitalRepository.GetAsync(x => x.Address == request.Address && x.Id != id) != null)
                throw new Exception("Hospital with such address already exists");

            var updateEntity = _mapper.Map<Hospital>(request);
            await _hospitalRepository.UpdateAsync(updateEntity);

            var result = _mapper.Map<HospitalDTO>(updateEntity);
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            var hospital = await _hospitalRepository.GetAsync(x => x.Id == id);

            if (hospital == null)
                throw new Exception($"Hospital with such id {id} not found for deletion");

            await _hospitalRepository.RemoveAsync(hospital);
        }

        public async Task<List<DoctorDTO>> GetDoctorsAsync(string id)
        {
            if (await _hospitalRepository.GetAsync(x => x.Id == id) == null)
                throw new Exception($"Hospital with such id {id} not found");

            var source = await _doctorRepository.GetAllAsync(x => x.HospitalId == id, includeProperties: "User", 
                                                             isTracking: false);

            var doctors = _mapper.Map<List<DoctorDTO>>(source);
            return doctors;
        }

        public async Task<List<PatientDTO>> GetPatientsAsync(string id)
        {
            if (await _hospitalRepository.GetAsync(x => x.Id == id) == null )
                throw new Exception($"Hospital with such id {id} not found");

            var source = await _patientRepository.GetAllAsync(x => x.HospitalId == id, includeProperties: "User",
                                                              isTracking: false);

            var result = _mapper.Map<List<PatientDTO>>(source);
            return result;
        }

        public async Task<List<HospitalAdministratorDTO>> GetAdministratorsAsync(string id)
        {
            if (await _hospitalRepository.GetAsync(x => x.Id == id) == null)
                throw new Exception($"Hospital with such id {id} not found");

            var source = await _hospitalAdministratorRepository.GetAllAsync(x => x.HospitalId == id, 
                                                                includeProperties: "User", isTracking: false); 

            var result = _mapper.Map<List<HospitalAdministratorDTO>>(source);
            return result;
        }
    }
}
