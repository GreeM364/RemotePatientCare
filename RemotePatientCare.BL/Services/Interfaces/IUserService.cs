using RemotePatientCare.BLL.DataTransferObjects;

namespace RemotePatientCare.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<ProfileDTO> GetProfileAsync(string userId);
    }
}
