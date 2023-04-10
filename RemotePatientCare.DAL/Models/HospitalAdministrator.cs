
namespace RemotePatientCare.DAL.Models
{
    public class HospitalAdministrator : BaseModel
    {
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public string HospitalId { get; set; } = null!;
        public Hospital Hospital { get; set; } = null!;
    }
}
