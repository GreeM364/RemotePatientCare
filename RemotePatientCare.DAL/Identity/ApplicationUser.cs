using Microsoft.AspNetCore.Identity;

namespace RemotePatientCare.DAL.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<ApplicationUserRole> UserRoles { get; set; }
    }
}