using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.DAL.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string id);

        Task<User> GetAsync(string id, string includeProperties = null);
    }
}
