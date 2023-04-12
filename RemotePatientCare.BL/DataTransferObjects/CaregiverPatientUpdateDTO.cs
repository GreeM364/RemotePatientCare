using System.ComponentModel.DataAnnotations;

namespace RemotePatientCare.BLL.DataTransferObjects
{
    public class CaregiverPatientUpdateDTO
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Patronymic { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
