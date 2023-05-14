namespace RemotePatientCare.DAL.Models
{
    public class PhysicalCondition : BaseModel
    {
        public int Pulse { get; set; }
        public int UpperArterialPressure { get; set; }
        public int LowerArterialPressure { get; set; }
        public double BodyTemperature { get; set; }
        public int BreathingRate { get; set; }


        public string PatientId { get; set; } = null!;
        public Patient Patient { get; set; } = null!;

    }
}
