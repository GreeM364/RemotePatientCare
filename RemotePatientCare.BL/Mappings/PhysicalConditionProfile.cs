﻿using AutoMapper;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.DAL.Models;

namespace RemotePatientCare.BLL.Mappings
{
    public class PhysicalConditionProfile : Profile
    {
        public PhysicalConditionProfile()
        {
            CreateMap<PhysicalConditionDTO, PhysicalCondition>()
            .ForMember(x => x.CreatedDate, o => o.MapFrom(s => s.Time))
            .ReverseMap();
            CreateMap<PhysicalCondition, PhysicalConditionCreateDTO>().ReverseMap();
        }
    }
}
