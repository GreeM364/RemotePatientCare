﻿using Microsoft.Extensions.DependencyInjection;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.BLL.Services;
using RemotePatientCare.DAL.Infrastructure;
using RemotePatientCare.BLL.Mappings;
using Microsoft.Extensions.Configuration;
using RemotePatientCare.BLL.BrainTree;

namespace RemotePatientCare.BLL.Infrastructure
{
    public static class BusinessLogicLayerExtensions
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataAccessLayer(configuration);

            services.AddAutoMapper(typeof(CaregiverPatientProfile));
            services.AddAutoMapper(typeof(DoctorProfile));
            services.AddAutoMapper(typeof(HospitalAdministratorProfile));
            services.AddAutoMapper(typeof(HospitalProfile));
            services.AddAutoMapper(typeof(PatientProfile));
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(PhysicalConditionProfile));
            services.AddAutoMapper(typeof(CriticalСonditionProfile));

            services.AddScoped<IHospitalService, HospitalService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IHospitalAdministratorService, HospitalAdministratorService>();
            services.AddScoped<ICaregiverPatientService, CaregiverPatientService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPhysicalConditionService, PhysicalConditionService>();
            services.AddScoped<ICriticalСonditionService, CriticalСonditionService>();

            services.Configure<BrainTreeSettings>(configuration.GetSection("BrainTree"));
            services.AddSingleton<IBrainTreeGate, BrainTreeGate>();

            return services;
        }
    }
}
