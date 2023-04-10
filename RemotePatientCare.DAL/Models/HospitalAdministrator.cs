
namespace RemotePatientCare.DAL.Models
{
    internal class HospitalAdministrator
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public string HospitalId { get; set; } = null!;
        public Hospital Hospital { get; set; } = null!;
    }
}
