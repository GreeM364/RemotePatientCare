using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RemotePatientCare.DAL.Data;
using RemotePatientCare.DAL.Repository.IRepository;
using RemotePatientCare.DAL.Repository;
using Microsoft.AspNetCore.Identity;
using RemotePatientCare.DAL.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace RemotePatientCare.DAL.Infrastructure
{
    public static class DataAccessLayerExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RemotePatientCareDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(ApplicationUserProfile));

            services.AddScoped<ICaregiverPatientRepository, CaregiverPatientRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IHospitalAdministratorRepository, HospitalAdministratorRepository>();
            services.AddScoped<IHospitalRepository, HospitalRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<RemotePatientCareDbContext>()
                    .AddDefaultTokenProviders();

            services.AddTransient<IdentityInitializer>();
            services.BuildServiceProvider().GetService<IdentityInitializer>().InitializeRolesAsync();

            services.AddScoped<RoleManager<ApplicationRole>>();

            services.AddSingleton<IConfiguration>(configuration);


            services.AddScoped<JwtHandler>();
            var key = configuration.GetValue<string>("JwtSettings:Secret");

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
