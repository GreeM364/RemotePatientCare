using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.BLL.Mappings
{
    public class HospitalProfile : Profile
    {
        public HospitalProfile() 
        {
            CreateMap<Hospital, HospitalDTO>().ReverseMap();
            CreateMap<Hospital, HospitalCreateDTO>().ReverseMap();
            CreateMap<Hospital, HospitalUpdateDTO>().ReverseMap();
        }
    }
}
