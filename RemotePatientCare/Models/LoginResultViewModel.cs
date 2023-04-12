namespace RemotePatientCare.API.Models
{
    public class LoginResultViewModel
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
    }
}
