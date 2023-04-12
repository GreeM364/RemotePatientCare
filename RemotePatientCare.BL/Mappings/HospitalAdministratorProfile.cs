using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.BLL.Mappings
{
    public class HospitalAdministratorProfile : Profile
    {
        public HospitalAdministratorProfile()
        {
            CreateMap<HospitalAdministrator, HospitalAdministratorDTO>()
            .ForMember(x => x.BirthDate, o => o.MapFrom(s => s.User.BirthDate))
            .ForMember(x => x.Email, o => o.MapFrom(s => s.User.Email))
            .ForMember(x => x.Phone, o => o.MapFrom(s => s.User.Phone))
            .ForMember(x => x.FirstName, o => o.MapFrom(s => s.User.FirstName))
            .ForMember(x => x.LastName, o => o.MapFrom(s => s.User.LastName))
            .ForMember(x => x.Patronymic, o => o.MapFrom(s => s.User.Patronymic))
            .ReverseMap();

            CreateMap<HospitalAdministratorCreateDTO, HospitalAdministrator>()
            .ForMember(x => x.User, o => o.MapFrom(s => s))
            .ReverseMap();

            CreateMap<HospitalAdministratorCreateDTO, User>().ReverseMap();

            CreateMap<HospitalAdministratorUpdateDTO, HospitalAdministrator>()
            .ForMember(x => x.User, o => o.MapFrom(s => s))
            .ReverseMap();

            CreateMap<HospitalAdministratorUpdateDTO, User>().ReverseMap();
        }
    }
}
