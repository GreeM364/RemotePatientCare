using RemotePatientCare.BLL.DataTransferObjects;

namespace RemotePatientCare.BLL.Services.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResultDTO> LoginAsync(LoginRequestDTO request);
    }
}
