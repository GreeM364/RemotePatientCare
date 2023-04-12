using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RemotePatientCare.DAL.Data;
using RemotePatientCare.DAL.Identity;
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
            //TODO entity.CreatedBy 

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
                throw new Exception("Error while creating patient account"); // TODO: probably create IdentityException
            }

            await _db.SaveChangesAsync();
        }

        public async Task<Patient> UpdateAsync(Patient entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            //TODO entity.LastModifiedBy

            _db.Patients.Update(entity);

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
