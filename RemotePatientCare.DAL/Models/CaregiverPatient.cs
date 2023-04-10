
namespace RemotePatientCare.DAL.Models
{
    internal class CaregiverPatient
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public List<Patient>? Patients { get; set; }
    }
}
