using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.BLL.Mappings
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientDTO>()
            .ForMember(x => x.BirthDate, o => o.MapFrom(s => s.User.BirthDate))
            .ForMember(x => x.Email, o => o.MapFrom(s => s.User.Email))
            .ForMember(x => x.Phone, o => o.MapFrom(s => s.User.Phone))
            .ForMember(x => x.FirstName, o => o.MapFrom(s => s.User.FirstName))
            .ForMember(x => x.LastName, o => o.MapFrom(s => s.User.LastName))
            .ForMember(x => x.Patronymic, o => o.MapFrom(s => s.User.Patronymic))
            .ReverseMap();

            CreateMap<PatientCreateDTO, Patient>()
            .ForMember(x => x.User, o => o.MapFrom(s => s))
            .ReverseMap();

            CreateMap<PatientCreateDTO, User>().ReverseMap();


            CreateMap<PatientUpdateDTO, Patient>()
            .ForMember(x => x.User, o => o.MapFrom(s => s))
            .ReverseMap();

            CreateMap<PatientUpdateDTO, User>().ReverseMap();
        }
    }
}
