using System.ComponentModel.DataAnnotations;

namespace RemotePatientCare.API.Models
{
    public class AddPatientToDoctorViewModel
    {
        [Required]
        public string PatientId { get; set; } = null!;
    }
}
