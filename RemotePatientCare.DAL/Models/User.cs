
namespace RemotePatientCare.DAL.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
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
