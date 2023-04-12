using Microsoft.Extensions.DependencyInjection;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.BLL.Services;
using RemotePatientCare.DAL.Infrastructure;

namespace RemotePatientCare.BLL.Infrastructure
{
    public static class BusinessLogicLayerExtensions
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDataAccessLayer(connectionString);
            services.AddAutoMapper(typeof(AutomapperBLLProfile));

            services.AddScoped<IHospitalService, HospitalService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IHospitalAdministratorService, HospitalAdministratorService>();
            services.AddScoped<ICaregiverPatientService, CaregiverPatientService>();

            return services;
        }
    }
}
