namespace RemotePatientCare.API.Models
{
    public class HospitalViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime DataPaySubscription { get; set; }

        public int DoctorsCount { get; set; }
        public int PatientsCount { get; set; }
    }
}
