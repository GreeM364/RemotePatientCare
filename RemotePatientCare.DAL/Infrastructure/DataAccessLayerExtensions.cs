using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RemotePatientCare.DAL.Data;

namespace RemotePatientCare.DAL.Infrastructure
{
    public static class DataAccessLayerExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RemotePatientCareDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
