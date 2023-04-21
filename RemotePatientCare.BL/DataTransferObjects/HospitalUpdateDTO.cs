namespace RemotePatientCare.BLL.DataTransferObjects
{
    public class HospitalUpdateDTO
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime DataPaySubscription { get; set; }
    }
}
