
namespace RemotePatientCare.DAL.Models
{
    internal class Patient
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public string? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public string? PatientCaretakerId { get; set; }
        public CaregiverPatient? CaregiverPatient { get; set; }

        public string HospitalId { get; set; } = null!;
        public Hospital Hospital { get; set; } = null!;
    }
}
