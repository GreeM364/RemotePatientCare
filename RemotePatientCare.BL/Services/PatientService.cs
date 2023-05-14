using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Exceptions;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.BLL.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ICaregiverPatientRepository _caregiverPatientRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, IHospitalRepository hospitalRepository,
                              IDoctorRepository doctorRepository, ICaregiverPatientRepository caregiverPatientRepository,
                              IMapper mapper)
        {
            _patientRepository = patientRepository;
            _hospitalRepository = hospitalRepository;
            _doctorRepository = doctorRepository;
            _caregiverPatientRepository = caregiverPatientRepository;
            _mapper = mapper;
        }

        public async Task<PatientDTO> GetByIdAsync(string id)
        {
            var source = await _patientRepository.GetAsync(x => x.Id == id, includeProperties: "User");

            if (source == null)
            {
                throw new NotFoundException($"Patient with id {id} not found");
            }

            return _mapper.Map<PatientDTO>(source);
        }

        public async Task<List<PatientDTO>> GetAsync()
        {
            var source = await _patientRepository.GetAllAsync(includeProperties: "User");
            return _mapper.Map<List<PatientDTO>>(source);
        }

        public async Task<PatientDTO> CreateAsync(PatientCreateDTO request)
        {
            if (await _hospitalRepository.GetAsync(x => x.Id == request.HospitalId) == null)
                throw new BadRequestException($"Hospital with id {request.HospitalId} doesn't exist");

            var existing = await _patientRepository.GetAsync(x => x.User.FirstName == request.FirstName
                                                                  && x.User.LastName == request.LastName
                                                                  && x.User.Phone == request.Phone);

            if (existing != null)
                throw new BadRequestException("Patient with such parameters already exists");

            if (!string.IsNullOrWhiteSpace(request.DoctorId))
            {
                if (await _doctorRepository.GetAsync(x => x.Id == request.DoctorId) == null)
                    throw new BadRequestException($"Doctor with id {request.DoctorId} doesn't exist");
            }

            var createEntity = _mapper.Map<Patient>(request);
            await _patientRepository.CreateAsync(createEntity, request.Password);

            var result = _mapper.Map<PatientDTO>(createEntity);
            return result;
        }

        public async Task<PatientDTO> UpdateAsync(string id, PatientUpdateDTO request)
        {
            var updateEntity = await _patientRepository.GetByIdAsync(id);

            if (request == null)
                throw new BadRequestException("The received model of Patient is null");

            if (updateEntity == null)
                throw new NotFoundException($"Patient with id {id} not found");

            _mapper.Map(request, updateEntity);
            await _patientRepository.UpdateAsync(updateEntity);

            var result = _mapper.Map<PatientDTO>(updateEntity);
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            var doctor = await _patientRepository.GetByIdAsync(id);

            if (doctor == null)
                throw new NotFoundException($"Patient with such id {id} not found for deletion");

            await _patientRepository.RemoveAsync(doctor);
        }

        public async Task<CaregiverPatientDTO> GetPatientCaretakerAsync(string id)
        {
            var patient = await _patientRepository.GetAsync(x => x.Id == id);
            if (patient == null)
                throw new NotFoundException($"Patient with id {id} not found");

            var source = await _caregiverPatientRepository.GetAsync(x => x.Id == patient.CaregiverPatientId,
                                                                includeProperties: "User", isTracking: false);
            if (source == null)
                return new CaregiverPatientDTO();

            var result = _mapper.Map<CaregiverPatientDTO>(source);
            return result;
        }

        public async Task<DoctorDTO> GetPatientDoctorAsync(string id)
        {
            var patient = await _patientRepository.GetAsync(x => x.Id == id);
            if (patient == null)
                throw new NotFoundException($"Patient with id {id} not found");

            var source = await _doctorRepository.GetAsync(x => x.Id == patient.DoctorId,
                                                                includeProperties: "User", isTracking: false);
            if (source == null)
                return new DoctorDTO();

            var result = _mapper.Map<DoctorDTO>(source);
            return result;
        }
    }
}
