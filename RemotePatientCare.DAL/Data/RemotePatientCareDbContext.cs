using RemotePatientCare.DAL.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.DAL.Data
{
    public class RemotePatientCareDbContext : IdentityDbContext<ApplicationUser>
    {
        public RemotePatientCareDbContext(DbContextOptions<RemotePatientCareDbContext> options) : base(options) { }

        public DbSet<User> BaseUsers { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<HospitalAdministrator> HospitalAdministrators { get; set; }
        public DbSet<CaregiverPatient> CaregiverPatients { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<PhysicalCondition> PhysicalConditions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationRoleConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new CaregiverPatientConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new HospitalAdministratorConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
        }
    }
}
