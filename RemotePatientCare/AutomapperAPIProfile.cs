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
            CreateMap<HospitalUpdateDTO, HospitalUpdateViewModel>().ReverseMap();

            CreateMap<DoctorDTO, DoctorViewModel>().ReverseMap();
            CreateMap<DoctorCreateDTO, DoctorCreateViewModel>().ReverseMap();
            CreateMap<DoctorUpdateDTO, DoctorUpdateViewModel>().ReverseMap();

            CreateMap<PatientDTO, PatientViewModel>().ReverseMap();
            CreateMap<PatientCreateDTO, PatientCreateViewModel>().ReverseMap();
            CreateMap<PatientUpdateDTO, PatientUpdateViewModel>().ReverseMap();

            CreateMap<HospitalAdministratorDTO, HospitalAdministratorViewModel>().ReverseMap();
            CreateMap<HospitalAdministratorCreateDTO, HospitalAdministratorCreateViewModel>().ReverseMap();
            CreateMap<HospitalAdministratorUpdateDTO, HospitalAdministratorUpdateViewModel>().ReverseMap();

            CreateMap<CaregiverPatientDTO, CaregiverPatientViewModel>().ReverseMap();
            CreateMap<CaregiverPatientCreateDTO, CaregiverPatientCreateViewModel>().ReverseMap();
            CreateMap<CaregiverPatientUpdateDTO, CaregiverPatientUpdateViewModel>().ReverseMap();

            CreateMap<LoginRequestDTO, LoginRequestViewModel>().ReverseMap();
            CreateMap<LoginResultDTO, LoginResultViewModel>().ReverseMap();

            CreateMap<ProfileDTO, ProfileViewModel>().ReverseMap();
            CreateMap<AddPatientToDoctorDTO, AddPatientToDoctorViewModel>().ReverseMap();
            CreateMap<AddPatientToCaregiverDTO, AddPatientToCaregiverViewModel>().ReverseMap();

            CreateMap<ClientTokenDTO, ClientTokenViewModel>().ReverseMap();
            CreateMap<PaymentNonceDTO, PaymentNonceViewModel>().ReverseMap();

            CreateMap<PhysicalConditionViewModel, PhysicalConditionDTO>().ReverseMap();

            CreateMap<CriticalСonditionViewModel, CriticalСonditionDTO>().ReverseMap();
        }
    }
}
