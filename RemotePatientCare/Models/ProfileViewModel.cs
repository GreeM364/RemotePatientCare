namespace RemotePatientCare.API.Models
{
    public class ProfileViewModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime BirthDate { get; set; }


        public DoctorViewModel? Doctor { get; set; }
        public PatientViewModel? Patient { get; set; }
        public CaregiverPatientViewModel? CaregiverPatient { get; set; }
        public HospitalAdministratorViewModel? HospitalAdministrator { get; set; }
    }
}
