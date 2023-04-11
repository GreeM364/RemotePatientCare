namespace RemotePatientCare.API.Models
{
    public class PatientViewModel
    {
        public string Id { get; set; } = null!;
        public string HospitalId { get; set; } = null!;
        public string? DoctorId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime BirthDate { get; set; }
    }
}
