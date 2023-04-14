using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Exceptions;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.BLL.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public DoctorService(IDoctorRepository doctorRepository, IHospitalRepository hospitalRepository,
                             IPatientRepository petientRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _hospitalRepository = hospitalRepository;
            _patientRepository = petientRepository;
            _mapper = mapper;
        }

        public async Task<DoctorDTO> GetByIdAsync(string id)
        {
            var source = await _doctorRepository.GetAsync(x => x.Id == id, "User");

            if (source == null)
            {
                throw new NotFoundException($"Doctor with id {id} not found");
            }

            return _mapper.Map<DoctorDTO>(source);
        }

        public async Task<List<DoctorDTO>> GetAsync()
        {
            var source = await _doctorRepository.GetAllAsync(includeProperties: "User");
            return _mapper.Map<List<DoctorDTO>>(source);
        }

        public async Task<DoctorDTO> CreateAsync(DoctorCreateDTO request)
        {
            if (await _hospitalRepository.GetAsync(x => x.Id == request.HospitalId) == null)
                throw new BadRequestException($"Hospital with id {request.HospitalId} doesn't exist");

            var existing = await _doctorRepository.GetAsync(x => x.User.FirstName == request.FirstName
                                                                  && x.User.LastName == request.LastName
                                                                  && x.User.Phone == request.Phone);

            if (existing != null)
                throw new BadRequestException("Doctor with such parameters already exists");

            var createEntity = _mapper.Map<Doctor>(request);
            await _doctorRepository.CreateAsync(createEntity, request.Password);

            var result = _mapper.Map<DoctorDTO>(createEntity);
            return result;
        }

        public async Task<DoctorDTO> UpdateAsync(string id, DoctorUpdateDTO request)
        {
            var updateEntity = await _doctorRepository.GetByIdAsync(id);

            if (request == null)
                throw new BadRequestException("The received model of Doctor is null");

            if (updateEntity == null)
                throw new NotFoundException($"Doctor with id {id} not found");


            _mapper.Map(request, updateEntity);
            await _doctorRepository.UpdateAsync(updateEntity);

            var result = _mapper.Map<DoctorDTO>(updateEntity);
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            var doctor = await _doctorRepository.GetAsync(x => x.Id == id);

            if (doctor == null)
                throw new NotFoundException($"Doctor with such id {id} not found for deletion");

            await _doctorRepository.RemoveAsync(doctor);
        }

        public async Task<List<PatientDTO>> GetPatientsAsync(string id)
        {
            if (await _doctorRepository.GetAsync(x => x.Id == id) == null)
                throw new NotFoundException($"Doctor with such id {id} not found");

            var source = await _patientRepository.GetAllAsync(x => x.DoctorId == id, includeProperties: "User",
                                                           isTracking: false);

            var result = _mapper.Map<List<PatientDTO>>(source);
            return result;
        }

        public async Task AddPatientToDoctorAsync(string doctorId, AddPatientToDoctorDTO request)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);

            if (patient == null)
                throw new NotFoundException($"Patient with id {request.PatientId} not found");

            if (await _doctorRepository.GetAsync(x => x.Id == doctorId) == null)
                throw new NotFoundException($"Doctor with id {doctorId} not found");

            patient.DoctorId = doctorId;
            await _patientRepository.UpdateAsync(patient);
        }

        public async Task DeletePatientToDoctorAsync(string patientId)
        {
            var patient = await _patientRepository.GetByIdAsync(patientId);

            if (patient == null)
                throw new NotFoundException($"Patient with id {patientId} not found");

            patient.DoctorId = null;
            await _patientRepository.UpdateAsync(patient);
        }
    }
}
