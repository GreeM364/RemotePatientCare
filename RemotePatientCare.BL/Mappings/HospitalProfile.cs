using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.BLL.Mappings
{
    public class HospitalProfile : Profile
    {
        public HospitalProfile() 
        {
            CreateMap<Hospital, HospitalDTO>()
                .ForMember(dest => dest.DoctorsCount, opt => opt.MapFrom(src => src.Doctors.Count()))
                .ForMember(dest => dest.PatientsCount, opt => opt.MapFrom(src => src.Patients.Count()))
                .ReverseMap();
            CreateMap<Hospital, HospitalCreateDTO>().ReverseMap();
            CreateMap<Hospital, HospitalUpdateDTO>().ReverseMap();
        }
    }
}
