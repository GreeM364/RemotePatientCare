using Microsoft.EntityFrameworkCore;
using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.DAL.Data
{
    public class RemotePatientCareDbContext : DbContext
    {
        public RemotePatientCareDbContext(DbContextOptions<RemotePatientCareDbContext> options) : base(options) { }

        public DbSet<User> BaseUsers { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<HospitalAdministrator> HospitalAdministrators { get; set; }
        public DbSet<CaregiverPatient> CaregiverPatients { get; set; }
    }
}
