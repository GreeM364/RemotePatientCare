using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RemotePatientCare.DAL.Data;
using RemotePatientCare.DAL.Repository.IRepository;
using RemotePatientCare.DAL.Repository;

namespace RemotePatientCare.DAL.Infrastructure
{
    public static class DataAccessLayerExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RemotePatientCareDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<ICaregiverPatientRepository, CaregiverPatientRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IHospitalAdministratorRepository, HospitalAdministratorRepository>();
            services.AddScoped<IHospitalRepository, HospitalRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();

            return services;
        }
    }
}
