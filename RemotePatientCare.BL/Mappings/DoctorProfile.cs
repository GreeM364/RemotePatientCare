using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.BLL.Mappings
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorDTO>()
            .ForMember(x => x.BirthDate, o => o.MapFrom(s => s.User.BirthDate))
            .ForMember(x => x.Email, o => o.MapFrom(s => s.User.Email))
            .ForMember(x => x.Phone, o => o.MapFrom(s => s.User.Phone))
            .ForMember(x => x.FirstName, o => o.MapFrom(s => s.User.FirstName))
            .ForMember(x => x.LastName, o => o.MapFrom(s => s.User.LastName))
            .ForMember(x => x.Patronymic, o => o.MapFrom(s => s.User.Patronymic))
            .ReverseMap();


            CreateMap<DoctorCreateDTO, Doctor>()
            .ForMember(x => x.User, o => o.MapFrom(s => s))
            .ReverseMap();

            CreateMap<DoctorCreateDTO, User>().ReverseMap();


            CreateMap<DoctorUpdateDTO, Doctor>()
            .ForMember(x => x.User, o => o.MapFrom(s => s));

            CreateMap<DoctorUpdateDTO, User>().ReverseMap();
        }
    }
}
