using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.BLL.Mappings
{
    public class PhysicalConditionProfile : Profile
    {
        public PhysicalConditionProfile()
        {
            CreateMap<PhysicalCondition, PhysicalConditionDTO>().ReverseMap();
            CreateMap<PhysicalCondition, PhysicalConditionCreateDTO>().ReverseMap();
        }
    }
}
