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
    internal class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private readonly RemotePatientCareDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public PatientRepository(RemotePatientCareDbContext db, UserManager<ApplicationUser> userManager,
                                 IMapper mapper) : base(db)
        {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task CreateAsync(Patient entity, string password)
        {
            entity.CreatedDate = DateTime.Now;

            _db.Patients.Add(entity);
            _db.BaseUsers.Add(entity.User);

            var applicationUser = _mapper.Map<User, ApplicationUser>(entity.User);
            var identityResult = await _userManager.CreateAsync(applicationUser, password);

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRolesAsync(applicationUser, new List<string>()
                {
                    CustomRoles.User,
                    CustomRoles.Patient,
                });
            }
            else
            {
                throw new IdentityException("Error while creating patient account"); 
            }

            await _db.SaveChangesAsync();
        }

        public override async Task RemoveAsync(Patient entity)
        {
            _db.BaseUsers.Remove(entity.User);
            _db.Patients.Remove(entity);

            await _db.SaveChangesAsync();
        }
    }
}
