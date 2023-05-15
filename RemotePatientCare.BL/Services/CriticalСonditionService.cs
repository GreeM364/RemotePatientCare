using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Exceptions;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.BLL.Services
{
    public class CriticalСonditionService : ICriticalСonditionService
    {
        private readonly ICriticalСonditionRepository _criticalСonditionRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public CriticalСonditionService(ICriticalСonditionRepository criticalСonditionRepository, IPatientRepository patientRepository,
                                        IMapper mapper)
        {
            _criticalСonditionRepository = criticalСonditionRepository;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CriticalСonditionCreateDTO request)
        {
            if (await _patientRepository.GetAsync(p => p.Id == request.PatientId) == null)
                throw new BadRequestException($"Patient with Id {request.PatientId} does exist");
            if (request == null)
                throw new BadRequestException("The request model of CriticalСondition is null");

            var createEntity = _mapper.Map<CriticalСondition>(request);
            await _criticalСonditionRepository.CreateAsync(createEntity);

            var result = _mapper.Map<CriticalСonditionDTO>(createEntity);
        }

        public async Task<List<CriticalСonditionDTO>> GetAsync()
        {
            var source = await _criticalСonditionRepository.GetAllAsync();
            return _mapper.Map<List<CriticalСonditionDTO>>(source);
        }

        public async Task<CriticalСonditionDTO> GetByIdAsync(string id)
        {
            var source = await _criticalСonditionRepository.GetByIdAsync(id);

            if (source == null)
            {
                throw new NotFoundException($"Critical Сondition with id {id} not found");
            }

            return _mapper.Map<CriticalСonditionDTO>(source);
        }

        public async Task<List<CriticalСonditionDTO>> GetCriticalСonditionByPatientAsync(string id)
        {
            if (await _patientRepository.GetAsync(x => x.Id == id) == null)
                throw new NotFoundException($"Patient with id {id} not found");

            var source = await _criticalСonditionRepository.GetAllAsync(x => x.PatientId == id);

            var criticalСondition = _mapper.Map<List<CriticalСonditionDTO>>(source);
            return criticalСondition;
        }
    }
}
