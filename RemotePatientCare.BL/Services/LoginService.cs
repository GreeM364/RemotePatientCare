using Microsoft.AspNetCore.Identity;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.DAL.Identity;
using RemotePatientCare.DAL.Infrastructure;
using RemotePatientCare.DAL.Repository.IRepository;
using RemotePatientCare.Utility;

namespace RemotePatientCare.BLL.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IHospitalAdministratorRepository _hospitalAdministratorRepository;
        private readonly JwtHandler _jwtHandler;

        public LoginService(UserManager<ApplicationUser> userManager, JwtHandler jwtHandler,
                            IDoctorRepository doctorRepository, IHospitalAdministratorRepository hospitalAdministratorRepository)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _doctorRepository = doctorRepository;
            _hospitalAdministratorRepository = hospitalAdministratorRepository;
        }
        public async Task<LoginResultDTO> LoginAsync(LoginRequestDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return new LoginResultDTO()
                {
                    ErrorMessage = "Invalid Authentication"
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            foreach(var role in roles)
            {
                if(role == CustomRoles.Doctor)
                {
                    var doctor = await _doctorRepository.GetAsync(d => d.UserId == user.Id, "Hospital");

                    bool checkSubscription = CheckSubscription(doctor.Hospital.DataPaySubscription);

                    if (!checkSubscription)
                        return new LoginResultDTO() { ErrorMessage = "Subscription expired" };

                    break;
                }
                if(role == CustomRoles.HospitalAdministrator)
                {
                    var admin = await _hospitalAdministratorRepository.GetAsync(a => a.UserId == user.Id, "Hospital");

                    bool checkSubscription = CheckSubscription(admin.Hospital.DataPaySubscription);

                    if (checkSubscription)
                        return new LoginResultDTO() { ErrorMessage = "Subscription expired"};

                    break;
                }
            }


            string token = await _jwtHandler.GenerateJwtToken(user);
            return new LoginResultDTO() { IsSuccess = true, Token = token };
        }

        private bool CheckSubscription(DateTime dataPaySubscription)
        {
            var check = (DateTime.Today - dataPaySubscription).TotalDays - 30;

            if (check > 0)
                return false;

            return true;
        }


    }
}
