namespace RemotePatientCare.DAL.Models
{
    public class Hospital : BaseModel
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime DataPaySubscription { get; set; }

        public List<Doctor>? Doctors { get; set; }
        public List<HospitalAdministrator>? Administrators { get; set; }
        public List<Patient>? Patients { get; set; }
    }
}
