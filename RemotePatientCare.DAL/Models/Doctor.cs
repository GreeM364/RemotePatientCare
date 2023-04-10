
namespace RemotePatientCare.DAL.Models
{
    internal class Doctor
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime BeginningWorkingDay { get; set; }
        public DateTime EndWorkingDay { get; set; }

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!; 

        public string HospitalId { get; set; } = null!;
        public Hospital Hospital { get; set; } = null!;

        public List<Patient>? Patients { get; set; }
    }
}
