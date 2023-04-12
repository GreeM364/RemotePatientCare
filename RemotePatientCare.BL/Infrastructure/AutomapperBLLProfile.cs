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
            CreateMap<Hospital, HospitalUpdateDTO>().ReverseMap();


            CreateMap<Doctor, DoctorDTO>()
            .ForMember(x => x.BirthDate, o => o.MapFrom(s => s.User.BirthDate))
            .ForMember(x => x.Email, o => o.MapFrom(s => s.User.Email))
            .ForMember(x => x.Phone, o => o.MapFrom(s => s.User.Phone))
            .ForMember(x => x.FirstName, o => o.MapFrom(s => s.User.FirstName))
            .ForMember(x => x.LastName, o => o.MapFrom(s => s.User.LastName))
            .ForMember(x => x.Patronymic, o => o.MapFrom(s => s.User.Patronymic)).ReverseMap();

            CreateMap<DoctorCreateDTO, Doctor>()
            .ForMember(x => x.User, o => o.MapFrom(s => s)).ReverseMap();
            CreateMap<DoctorCreateDTO, User>().ReverseMap();

            CreateMap<DoctorUpdateDTO, Doctor>()
            .ForMember(x => x.User, o => o.MapFrom(s => s));
            CreateMap<DoctorUpdateDTO, User>().ReverseMap();



            CreateMap<Patient, PatientDTO>()
                .ForMember(x => x.BirthDate, o => o.MapFrom(s => s.User.BirthDate))
                .ForMember(x => x.Email, o => o.MapFrom(s => s.User.Email))
                .ForMember(x => x.Phone, o => o.MapFrom(s => s.User.Phone))
                .ForMember(x => x.FirstName, o => o.MapFrom(s => s.User.FirstName))
                .ForMember(x => x.LastName, o => o.MapFrom(s => s.User.LastName))
                .ForMember(x => x.Patronymic, o => o.MapFrom(s => s.User.Patronymic)).ReverseMap();

            CreateMap<PatientCreateDTO, Patient>()
            .ForMember(x => x.User, o => o.MapFrom(s => s)).ReverseMap();
            CreateMap<PatientCreateDTO, User>().ReverseMap();

            CreateMap<PatientUpdateDTO, Patient>()
                .ForMember(x => x.User, o => o.MapFrom(s => s)).ReverseMap();
            CreateMap<PatientUpdateDTO, User>().ReverseMap();



            CreateMap<HospitalAdministrator, HospitalAdministratorDTO>()
                .ForMember(x => x.BirthDate, o => o.MapFrom(s => s.User.BirthDate))
                .ForMember(x => x.Email, o => o.MapFrom(s => s.User.Email))
                .ForMember(x => x.Phone, o => o.MapFrom(s => s.User.Phone))
                .ForMember(x => x.FirstName, o => o.MapFrom(s => s.User.FirstName))
                .ForMember(x => x.LastName, o => o.MapFrom(s => s.User.LastName))
                .ForMember(x => x.Patronymic, o => o.MapFrom(s => s.User.Patronymic)).ReverseMap();

            CreateMap<HospitalAdministratorCreateDTO, HospitalAdministrator>()
            .ForMember(x => x.User, o => o.MapFrom(s => s)).ReverseMap();
            CreateMap<HospitalAdministratorCreateDTO, User>().ReverseMap();

            CreateMap<HospitalAdministratorUpdateDTO, HospitalAdministrator>()
                .ForMember(x => x.User, o => o.MapFrom(s => s)).ReverseMap();
            CreateMap<HospitalAdministratorUpdateDTO, User>().ReverseMap();




            CreateMap<CaregiverPatient, CaregiverPatientDTO>()
                .ForMember(x => x.BirthDate, o => o.MapFrom(s => s.User.BirthDate))
                .ForMember(x => x.Email, o => o.MapFrom(s => s.User.Email))
                .ForMember(x => x.Phone, o => o.MapFrom(s => s.User.Phone))
                .ForMember(x => x.FirstName, o => o.MapFrom(s => s.User.FirstName))
                .ForMember(x => x.LastName, o => o.MapFrom(s => s.User.LastName))
                .ForMember(x => x.Patronymic, o => o.MapFrom(s => s.User.Patronymic)).ReverseMap();

            CreateMap<CaregiverPatientCreateDTO, CaregiverPatient>()
            .ForMember(x => x.User, o => o.MapFrom(s => s)).ReverseMap();
            CreateMap<CaregiverPatientCreateDTO, User>().ReverseMap();

            CreateMap<CaregiverPatientUpdateDTO, CaregiverPatient>()
            .ForMember(x => x.User, o => o.MapFrom(s => s)).ReverseMap();
            CreateMap<CaregiverPatientUpdateDTO, User>().ReverseMap();
        }
    }
}
