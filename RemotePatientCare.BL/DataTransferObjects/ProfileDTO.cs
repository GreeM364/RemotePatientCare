namespace RemotePatientCare.BLL.DataTransferObjects
{
    public class ProfileDTO
    {
        public DoctorDTO? Doctor { get; set; }
        public PatientDTO? Patient { get; set; }
        public CaregiverPatientDTO? PatientCaretaker { get; set; }
        public HospitalAdministratorDTO? HospitalAdministrator { get; set; }
    }
}
