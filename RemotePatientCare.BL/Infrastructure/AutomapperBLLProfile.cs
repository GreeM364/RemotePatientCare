using AutoMapper;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.BLL.DataTransferObjects;

namespace RemotePatientCare.BLL.Infrastructure
{
    public class AutomapperBLLProfile : Profile
    {
        public AutomapperBLLProfile()
        {
            CreateMap<Hospital, HospitalDTO>().ForMember(dest => dest.DoctorsCount, 
                opt => opt.MapFrom(src => src.Doctors.Count())).ReverseMap();
            CreateMap<Hospital, HospitalCreateDTO>().ReverseMap();
            CreateMap<Hospital, HospitalUpdateDTO>();
        }
    }
}
