namespace RemotePatientCare.API.Models
{
    public class ProfileViewModel
    {
        public DoctorViewModel? Doctor { get; set; }
        public PatientViewModel? Patient { get; set; }
        public CaregiverPatientViewModel? PatientCaretaker { get; set; }
        public HospitalAdministratorViewModel? HospitalAdministrator { get; set; }
    }
}
