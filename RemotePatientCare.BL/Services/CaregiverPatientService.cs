using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Exceptions;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.BLL.Services
{
    public class CaregiverPatientService : ICaregiverPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ICaregiverPatientRepository _caregiverPatientRepository;
        private readonly IMapper _mapper;

        public CaregiverPatientService(IPatientRepository patientRepository, ICaregiverPatientRepository caregiverPatientRepository,
                                       IMapper mapper)
        {
            _caregiverPatientRepository = caregiverPatientRepository;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<CaregiverPatientDTO> GetByIdAsync(string id)
        {
            var source = await _caregiverPatientRepository.GetAsync(x => x.Id == id, "User");

            if (source == null)
            {
                throw new NotFoundException($"Patient Caretaker with id {id} not found");
            }

            return _mapper.Map<CaregiverPatientDTO>(source);
        }

        public async Task<List<CaregiverPatientDTO>> GetAsync()
        {
            var source = await _caregiverPatientRepository.GetAllAsync(includeProperties: "User");
            return _mapper.Map<List<CaregiverPatientDTO>>(source);
        }

        public async Task<CaregiverPatientDTO> CreateAsync(CaregiverPatientCreateDTO request)
        {
            var existing = await _caregiverPatientRepository.GetAsync(x => x.User.FirstName == request.FirstName
                                                                                && x.User.LastName == request.LastName
                                                                                && x.User.Phone == request.Phone);

            if (existing != null)
                throw new BadRequestException("Caregiver Patient with such parameters already exists");

            var createEntity = _mapper.Map<CaregiverPatient>(request);
            await _caregiverPatientRepository.CreateAsync(createEntity, request.Password);

            var result = _mapper.Map<CaregiverPatientDTO>(createEntity);
            return result;
        }

        public async Task<CaregiverPatientDTO> UpdateAsync(string id, CaregiverPatientUpdateDTO request)
        {
            var updateEntity = await _caregiverPatientRepository.GetByIdAsync(id);

            if (request == null)
                throw new BadRequestException("The received model of Caregiver Patient is null");

            if (updateEntity == null)
                throw new NotFoundException($"Caregiver Patient with id {id} not found");


            _mapper.Map(request, updateEntity);
            await _caregiverPatientRepository.UpdateAsync(updateEntity);

            var result = _mapper.Map<CaregiverPatientDTO>(updateEntity);
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            var caregiverPatient = await _caregiverPatientRepository.GetAsync(c => c.Id == id, "User");

            if (caregiverPatient == null)
                throw new NotFoundException($"Caregiver Patient with such id {id} not found for deletion");

            await _caregiverPatientRepository.RemoveAsync(caregiverPatient);
        }

        public async Task<List<PatientDTO>> GetPatientsAsync(string id)
        {
            if (await _caregiverPatientRepository.GetAsync(x => x.Id == id) == null)
                throw new NotFoundException($"Caregiver Patient with such id {id} not found");

            var source = await _patientRepository.GetAllAsync(x => x.CaregiverPatientId == id, includeProperties: "User",
                                                              isTracking: false);

            var result = _mapper.Map<List<PatientDTO>>(source);
            return result;
        }

        public async Task AddPatientToCaregiverAsync(string caregiverPatientId, AddPatientToCaregiverDTO request)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);

            if (patient == null)
                throw new NotFoundException($"Patient with id {request.PatientId} not found");

            if (await _caregiverPatientRepository.GetAsync(x => x.Id == caregiverPatientId) == null)
                throw new NotFoundException($"Caregiver Patient with id {caregiverPatientId} not found");

            patient.CaregiverPatientId = caregiverPatientId;
            await _patientRepository.UpdateAsync(patient);
        }

        public async Task DeletePatientToCaregiverAsync(string patientId)
        {
            var patient = await _patientRepository.GetByIdAsync(patientId);

            if (patient == null)
                throw new NotFoundException($"Patient with id {patientId} not found");

            patient.CaregiverPatientId = null;
            await _patientRepository.UpdateAsync(patient);
        }
    }
}
