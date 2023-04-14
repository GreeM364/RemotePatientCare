using System.ComponentModel.DataAnnotations;

namespace RemotePatientCare.API.Models
{
    public class AddPatientToCaregiverViewModel
    {
        [Required]
        public string PatientId { get; set; } = null!;
    }
}
