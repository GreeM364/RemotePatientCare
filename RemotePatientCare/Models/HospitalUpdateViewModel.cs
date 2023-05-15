using System.ComponentModel.DataAnnotations;

namespace RemotePatientCare.API.Models
{
    public class HospitalUpdateViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        public DateTime DataPaySubscription { get; set; }
    }
}
