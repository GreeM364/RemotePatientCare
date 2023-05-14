namespace RemotePatientCare.IoT.Models
{
    public class IndicatorsPhysicalConditionAverage
    {
        public string PatientId { get; set; } = null!;
        public int Pulse { get; set; }
        public int UpperArterialPressure { get; set; }
        public int LowerArterialPressure { get; set; }
        public double BodyTemperature { get; set; }
        public int BreathingRate { get; set; }
    }
}
