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

            CreateMap<DoctorDTO, DoctorViewModel>().ReverseMap();
            CreateMap<DoctorCreateDTO, DoctorCreateViewModel>().ReverseMap();
            CreateMap<DoctorUpdateDTO, DoctorUpdateViewModel>().ReverseMap();

            CreateMap<PatientDTO, PatientViewModel>().ReverseMap();
            CreateMap<PatientCreateDTO, PatientCreateViewModel>().ReverseMap();
            CreateMap<PatientUpdateDTO, PatientUpdateViewModel>().ReverseMap();

            CreateMap<HospitalAdministratorDTO, HospitalAdministratorViewModel>().ReverseMap();
            CreateMap<HospitalAdministratorCreateDTO, HospitalAdministratorCreateViewModel>().ReverseMap();
            CreateMap<HospitalAdministratorUpdateDTO, HospitalAdministratorUpdateViewModel>().ReverseMap();
        }
    }
}
