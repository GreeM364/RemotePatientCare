using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Exceptions;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.BLL.Services
{
    public class PhysicalConditionService : IPhysicalConditionService
    {
        private readonly IPhysicalConditionRepository _physicalConditionRepositort;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PhysicalConditionService(IPhysicalConditionRepository physicalConditionRepositort, IPatientRepository patientRepository,
                                        IMapper mapper)
        {
            _physicalConditionRepositort = physicalConditionRepositort;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(PhysicalConditionCreateDTO request)
        {
            if (await _patientRepository.GetAsync(p => p.Id == request.PatientId) == null)
                throw new BadRequestException($"Patient with Id {request.PatientId} does exist");
            if (request == null)
                throw new BadRequestException("The request model of PhysicalCondition is null");

            var createEntity = _mapper.Map<PhysicalCondition>(request);
            await _physicalConditionRepositort.CreateAsync(createEntity);

            var result = _mapper.Map<PhysicalConditionDTO>(createEntity);
        }

        public async Task<List<PhysicalConditionDTO>> GetAsync()
        {
            var source = await _physicalConditionRepositort.GetAllAsync();
            return _mapper.Map<List<PhysicalConditionDTO>>(source);
        }

        public async Task<PhysicalConditionDTO> GetByIdAsync(string id)
        {
            var source = await _physicalConditionRepositort.GetByIdAsync(id);

            if (source == null)
            {
                throw new NotFoundException($"Physical Condition with id {id} not found");
            }

            return _mapper.Map<PhysicalConditionDTO>(source);
        }

        public async Task<List<PhysicalConditionDTO>> GetPhysicalConditionByPatientAsync(string id)
        {
            if (await _patientRepository.GetAsync(x => x.Id == id) == null)
                throw new NotFoundException($"Patient with id {id} not found");

            var source = await _physicalConditionRepositort.GetAllAsync(x => x.PatientId == id);

            var physicalCondition = _mapper.Map<List<PhysicalConditionDTO>>(source);
            return physicalCondition;
        }
    }
}
