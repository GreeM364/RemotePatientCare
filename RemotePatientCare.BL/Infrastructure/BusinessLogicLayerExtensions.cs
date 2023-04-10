using Microsoft.Extensions.DependencyInjection;
using RemotePatientCare.DAL.Infrastructure;

namespace RemotePatientCare.BLL.Infrastructure
{
    public static class BusinessLogicLayerExtensions
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection service, string connectionString)
        {
            service.AddDataAccessLayer(connectionString);

            return service;
        }
    }
}
