using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RemotePatientCare.DAL.Data;
using RemotePatientCare.DAL.Identity;
using RemotePatientCare.DAL.Infrastructure;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Repository.IRepository;
using RemotePatientCare.Utility;

namespace RemotePatientCare.DAL.Repository
{
    public class HospitalAdministratorRepository : Repository<HospitalAdministrator>, IHospitalAdministratorRepository
    {
        private readonly RemotePatientCareDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public HospitalAdministratorRepository(RemotePatientCareDbContext db, UserManager<ApplicationUser> userManager,
                                               IMapper mapper) : base(db)
        {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task CreateAsync(HospitalAdministrator entity, string password)
        {
            entity.CreatedDate = DateTime.Now;

            _db.HospitalAdministrators.Add(entity);
            _db.BaseUsers.Add(entity.User);

            var applicationUser = _mapper.Map<User, ApplicationUser>(entity.User);
            var identityResult = await _userManager.CreateAsync(applicationUser, password);

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRolesAsync(applicationUser, new List<string>()
                {
                    CustomRoles.User,
                    CustomRoles.HospitalAdministrator,
                });
            }
            else
            {
                throw new IdentityException("Error while creating hospital administrator account"); 
            }

            await _db.SaveChangesAsync();
        }

        public async Task<HospitalAdministrator> UpdateAsync(HospitalAdministrator entity)
        {
            entity.LastModifiedDate = DateTime.Now;

            _db.HospitalAdministrators.Update(entity);

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
