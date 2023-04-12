using Microsoft.AspNetCore.Identity;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.DAL.Identity;
using RemotePatientCare.DAL.Infrastructure;

namespace RemotePatientCare.BLL.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtHandler _jwtHandler;

        public LoginService(UserManager<ApplicationUser> userManager, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
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

            string token = await _jwtHandler.GenerateJwtToken(user);
            return new LoginResultDTO() { IsSuccess = true, Token = token };
        }
    }
}
