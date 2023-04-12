namespace RemotePatientCare.BLL.DataTransferObjects
{
   public class LoginResultDTO
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
    }
}
