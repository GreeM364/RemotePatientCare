namespace RemotePatientCare.API.Models
{
    public class PhysicalConditionViewModel
    {
        public string Id { get; set; } = null!;
        public string PatientId { get; set; } = null!;
        public int Pulse { get; set; }
        public int UpperArterialPressure { get; set; }
        public int LowerArterialPressure { get; set; }
        public double BodyTemperature { get; set; }
        public int BreathingRate { get; set; }
        public DateTime DateTime { get; set; }
    }
}
