using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Exceptions;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.DAL.Repository.IRepository;

namespace RemotePatientCare.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ProfileDTO> GetProfileAsync(string userId)
        {
            var source = await _userRepository.GetAsync(userId, "Doctor,Patient,CaregiverPatient,HospitalAdministrator");

            if (source == null)
                throw new NotFoundException($"User with id {userId} not found");

            var result = _mapper.Map<ProfileDTO>(source);
            return result;
        }
    }
}