using System.ComponentModel.DataAnnotations;

namespace RemotePatientCare.API.Models
{
    public class LoginRequestViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = null!;
    }
}
