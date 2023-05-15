namespace RemotePatientCare.API.Models
{
    public class CriticalСonditionViewModel
    {
        public string Id { get; set; } = null!;
        public string PatientId { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime Time { get; set; }
    }
}
