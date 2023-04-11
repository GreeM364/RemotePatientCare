using AutoMapper;
using RemotePatientCare.API.Models;
using RemotePatientCare.BLL.DataTransferObjects;

namespace RemotePatientCare.API
{
    public class AutomapperAPIProfile : Profile
    {
        public AutomapperAPIProfile()
        {
            CreateMap<HospitalDTO, HospitalViewModel>().ReverseMap();
            CreateMap<HospitalCreateDTO, HospitalCreateViewModel>().ReverseMap();
            CreateMap<HospitalUpdateViewModel, HospitalUpdateViewModel>().ReverseMap();
        }
    }
}
