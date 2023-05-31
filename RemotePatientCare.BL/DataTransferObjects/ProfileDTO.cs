namespace RemotePatientCare.BLL.DataTransferObjects
{
    public class ProfileDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime BirthDate { get; set; }

        public DoctorDTO? Doctor { get; set; }
        public PatientDTO? Patient { get; set; }
        public CaregiverPatientDTO? CaregiverPatient { get; set; }
        public HospitalAdministratorDTO? HospitalAdministrator { get; set; }
    }
}
