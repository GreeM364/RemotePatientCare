using AutoMapper;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.BLL.DataTransferObjects;

namespace RemotePatientCare.BLL.Infrastructure
{
    public class AutomapperBLLProfile : Profile
    {
        public AutomapperBLLProfile()
        {
            CreateMap<Hospital, HospitalDTO>()
                .ForMember(dest => dest.DoctorsCount, opt => opt.MapFrom(src => src.Doctors.Count()))
                .ForMember(dest => dest.PatientsCount, opt => opt.MapFrom(src => src.Patients.Count()))
                .ReverseMap();
            CreateMap<Hospital, HospitalCreateDTO>().ReverseMap();
            CreateMap<Hospital, HospitalUpdateDTO>();

            CreateMap<Doctor, DoctorDTO>()
            .ForMember(x => x.BirthDate, o => o.MapFrom(s => s.User.BirthDate))
            .ForMember(x => x.Email, o => o.MapFrom(s => s.User.Email))
            .ForMember(x => x.Phone, o => o.MapFrom(s => s.User.Phone))
            .ForMember(x => x.FirstName, o => o.MapFrom(s => s.User.FirstName))
            .ForMember(x => x.LastName, o => o.MapFrom(s => s.User.LastName))
            .ForMember(x => x.Patronymic, o => o.MapFrom(s => s.User.Patronymic));

            CreateMap<DoctorCreateDTO, Doctor>()
            .ForMember(x => x.User, o => o.MapFrom(s => s)).ReverseMap();
            CreateMap<DoctorCreateDTO, User>().ReverseMap();

            CreateMap<DoctorCreateDTO, Doctor>()
            .ForMember(x => x.User, o => o.MapFrom(s => s));
            CreateMap<DoctorCreateDTO, User>();
        }
    }
}
