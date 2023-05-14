namespace RemotePatientCare.IoT.Models
{
    public class IndicatorsCriticalСondition
    {
        public string PatientId { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime DateTime { get; set; }
    }
}
