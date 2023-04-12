namespace RemotePatientCare.DAL.Models
{
    public class CaregiverPatient : BaseModel
    {
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public List<Patient>? Patients { get; set; }
    }
}
