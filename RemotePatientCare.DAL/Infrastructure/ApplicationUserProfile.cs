using AutoMapper;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.DAL.Identity;

namespace RemotePatientCare.DAL.Infrastructure
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<User, ApplicationUser>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.Phone));
        }
    }
}
