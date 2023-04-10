
namespace RemotePatientCare.DAL.Models
{
    internal class User : BaseModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime BirthDate { get; set; }

        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
        public CaregiverPatient? CaregiverPatient { get; set; }
        public HospitalAdministrator? HospitalAdministrator { get; set; }
    }
}
