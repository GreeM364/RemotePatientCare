using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RemotePatientCare.DAL.Data;
using RemotePatientCare.DAL.Repository.IRepository;
using RemotePatientCare.DAL.Repository;
using Microsoft.AspNetCore.Identity;
using RemotePatientCare.DAL.Identity;


namespace RemotePatientCare.DAL.Infrastructure
{
    public static class DataAccessLayerExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RemotePatientCareDbContext>(options => options.UseSqlServer(connectionString));
            services.AddAutoMapper(typeof(ApplicationUserProfile));

            services.AddScoped<ICaregiverPatientRepository, CaregiverPatientRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IHospitalAdministratorRepository, HospitalAdministratorRepository>();
            services.AddScoped<IHospitalRepository, HospitalRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<RemotePatientCareDbContext>()
                    .AddDefaultTokenProviders();

            services.AddTransient<IdentityInitializer>();
            services.BuildServiceProvider().GetService<IdentityInitializer>().InitializeRolesAsync();

            services.AddScoped<RoleManager<ApplicationRole>>();

            return services;
        }
    }
}
