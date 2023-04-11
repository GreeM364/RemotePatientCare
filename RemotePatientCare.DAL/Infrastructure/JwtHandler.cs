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
        private readonly IRepository<User> _userRepository;

        public JwtHandler(UserManager<ApplicationUser> userManager, IRepository<User> userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public async Task<string> GenerateJwtToken(IConfiguration configuration, ApplicationUser requestUser)
        {
            var user1 = await _userManager.FindByEmailAsync(requestUser.Email!);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Secret"]);

            var claims = await GetClaimsAsync(user1!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
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

            var domainUser = await _userRepository.GetByIdAsync(user.Id);

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
