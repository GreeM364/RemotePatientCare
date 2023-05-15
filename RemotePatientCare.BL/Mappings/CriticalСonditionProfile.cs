using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.BLL.Mappings
{
    public class CriticalСonditionProfile : Profile
    {
        public CriticalСonditionProfile()
        {
            CreateMap<CriticalСonditionDTO, CriticalСondition>()
            .ForMember(x => x.CreatedDate, o => o.MapFrom(s => s.Time))
            .ReverseMap();
            CreateMap<CriticalСondition, CriticalСonditionCreateDTO>().ReverseMap();
        }
    }
}
