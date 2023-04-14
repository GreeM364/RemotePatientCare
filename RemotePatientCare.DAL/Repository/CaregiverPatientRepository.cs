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
    public class CaregiverPatientRepository : Repository<CaregiverPatient>, ICaregiverPatientRepository
    {
        private readonly RemotePatientCareDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public CaregiverPatientRepository(RemotePatientCareDbContext db, UserManager<ApplicationUser> userManager,
                                          IMapper mapper) : base(db)
        {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task CreateAsync(CaregiverPatient entity, string password)
        {
            entity.CreatedDate = DateTime.Now;

            _db.CaregiverPatients.Add(entity);
            _db.BaseUsers.Add(entity.User);

            var applicationUser = _mapper.Map<User, ApplicationUser>(entity.User);
            var identityResult = await _userManager.CreateAsync(applicationUser, password);

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRolesAsync(applicationUser, new List<string>()
                {
                    CustomRoles.User,
                    CustomRoles.CaregiverPatient,
                });
            }
            else
            {
                throw new IdentityException("Error while creating caregiver patient account");
            }

            await _db.SaveChangesAsync();
        }

        public async Task<CaregiverPatient> UpdateAsync(CaregiverPatient entity)
        {
            entity.LastModifiedDate = DateTime.Now;

            _db.CaregiverPatients.Update(entity);

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
