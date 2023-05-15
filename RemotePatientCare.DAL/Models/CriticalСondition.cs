namespace RemotePatientCare.DAL.Models
{
    public class CriticalСondition : BaseModel
    {
        public string Message { get; set; } = null!;

        public string PatientId { get; set; } = null!;
        public Patient Patient { get; set; } = null!;
    }
}
