using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RemotePatientCare.DAL.Identity;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;
using RemotePatientCare.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RemotePatientCare.DAL.Infrastructure
{
    public class JwtHandler
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public JwtHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public async Task<string> GenerateJwtToken(ApplicationUser requestUser)
        {
            var user = await _userManager.FindByEmailAsync(requestUser.Email!);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _configuration.GetValue<string>("JwtSettings:Secret"); 

            var claims = await GetClaimsAsync(user!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private async Task<List<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var domainUser = await _userRepository.GetAsync(user.Id, "Doctor,Patient,CaregiverPatient,HospitalAdministrator");

            if (domainUser?.Doctor != null)
                claims.Add(new Claim(CustomRoles.Doctor, domainUser.Doctor.Id));

            if (domainUser?.Patient != null)
                claims.Add(new Claim(CustomRoles.Patient, domainUser.Patient.Id));

            if (domainUser?.CaregiverPatient != null)
                claims.Add(new Claim(CustomRoles.CaregiverPatient, domainUser.CaregiverPatient.Id));

            if (domainUser?.HospitalAdministrator != null)
                claims.Add(new Claim(CustomRoles.HospitalAdministrator, domainUser.HospitalAdministrator.Id));

            return claims;
        }
    }
}
