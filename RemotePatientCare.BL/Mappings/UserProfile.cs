using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.BLL.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, ProfileDTO>().ReverseMap();
        }
    }
}
