namespace RemotePatientCare.BLL.DataTransferObjects
{
    public class CriticalСonditionDTO
    {
        public string Id { get; set; } = null!;
        public string PatientId { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime Time { get; set; }
    }
}
