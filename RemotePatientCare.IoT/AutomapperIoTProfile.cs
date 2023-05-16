using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.IoT.Models;

namespace RemotePatientCare.IoT
{
    public class AutomapperIoTProfile : Profile
    {
        public AutomapperIoTProfile()
        {
            CreateMap<PhysicalConditionCreateDTO, IndicatorsPhysicalConditionAverage>().ReverseMap();
            CreateMap<CriticalСonditionCreateDTO, IndicatorsCriticalСondition>().ReverseMap();
        }
    }
}
