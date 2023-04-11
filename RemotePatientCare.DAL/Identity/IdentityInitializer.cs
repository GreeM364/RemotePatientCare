using Microsoft.AspNetCore.Identity;
using RemotePatientCare.DAL.Identity;
using RemotePatientCare.Utility;

public class IdentityInitializer 
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public IdentityInitializer(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task InitializeRolesAsync()
    {
        var roles = new List<ApplicationRole>
        {
            new ApplicationRole { Name = CustomRoles.HospitalAdministrator },
            new ApplicationRole { Name = CustomRoles.User },
            new ApplicationRole { Name = CustomRoles.Doctor },
            new ApplicationRole { Name = CustomRoles.Patient },
            new ApplicationRole { Name = CustomRoles.CaregiverPatient },
            new ApplicationRole { Name = CustomRoles.GlobalAdmin }
        };

        foreach (var role in roles)
        {
            if (await _roleManager.RoleExistsAsync(role.Name)) 
                continue;

            await _roleManager.CreateAsync(role);
        }
    }
}
