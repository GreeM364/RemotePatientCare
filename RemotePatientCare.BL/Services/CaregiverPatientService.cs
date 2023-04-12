using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;
using System.ComponentModel.DataAnnotations;

namespace RemotePatientCare.BLL.Services
{
    public class CaregiverPatientService : ICaregiverPatientService
    {
        private readonly IPatientRepository _petientRepository;
        private readonly ICaregiverPatientRepository _caregiverPatientRepository;
        private readonly IMapper _mapper;

        public CaregiverPatientService(IPatientRepository petientRepository, ICaregiverPatientRepository caregiverPatientRepository,
                                       IMapper mapper)
        {
            _caregiverPatientRepository = caregiverPatientRepository;
            _petientRepository = petientRepository;
            _mapper = mapper;
        }

        public async Task<CaregiverPatientDTO> GetByIdAsync(string id)
        {
            var source = await _caregiverPatientRepository.GetAsync(x => x.Id == id, "User");

            if (source == null)
            {
                throw new Exception($"PatientCaretaker with id {id} not found");
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
                throw new ValidationException("Caregiver Patient with such parameters already exists");

            var createEntity = _mapper.Map<CaregiverPatient>(request);
            await _caregiverPatientRepository.CreateAsync(createEntity, request.Password);

            var result = _mapper.Map<CaregiverPatientDTO>(createEntity);
            return result;
        }

        public async Task<CaregiverPatientDTO> UpdateAsync(string id, CaregiverPatientUpdateDTO request)
        {
            if (request == null)
                throw new Exception("The received model of Caregiver Patient is null");

            if (await _caregiverPatientRepository.GetAsync(x => x.Id == id) != null)
                throw new Exception($"Caregiver Patient with id {id} not found");


            var updateEntity = _mapper.Map<CaregiverPatient>(request);
            await _caregiverPatientRepository.UpdateAsync(updateEntity);

            var result = _mapper.Map<CaregiverPatientDTO>(updateEntity);
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            var caregiverPatient = await _caregiverPatientRepository.GetAsync(x => x.Id == id);

            if (caregiverPatient == null)
                throw new Exception($"Caregiver Patient with such id {id} not found for deletion");

            await _caregiverPatientRepository.RemoveAsync(caregiverPatient);
        }

        public async Task<List<PatientDTO>> GetPatientsAsync(string id)
        {
            if (await _caregiverPatientRepository.GetAsync(x => x.Id == id) == null)
                throw new Exception($"Caregiver Patient with such id {id} not found");

            var source = await _petientRepository.GetAllAsync(x => x.CaregiverPatientId == id, includeProperties: "User",
                                                              isTracking: false);

            var result = _mapper.Map<List<PatientDTO>>(source);
            return result;
        }
    }
}
