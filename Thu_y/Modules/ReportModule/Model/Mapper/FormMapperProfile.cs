﻿using AutoMapper;
using Thu_y.Modules.ReportModule.Core;

namespace Thu_y.Modules.ReportModule.Model.Mapper
{
    public class FormMapperProfile: Profile
    {
        public FormMapperProfile()
        {
            CreateMap<FormModel, FormEntity>()
                .ForMember(x => x.FormAttributes, opt => opt.MapFrom(y=>y.Attributes))
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AttributeModel, FormAttributeEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Form, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ReportModel, ReportTicketEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Values, opt => opt.MapFrom(y => y.Values))
                .ForMember(x => x.ListAnimals, opt => opt.MapFrom(y => y.ListAnimals))
                .ForMember(x => x.SealTabs, opt => opt.MapFrom(y => y.SealTabs))
                .ReverseMap();

            CreateMap<ReportValueModel, ReportTicketValueEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x=>x.ReportTicket,opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ListAnimalModel, ListAnimalEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.ReportTicket, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<SealTabModel, SealTabEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.ReportTicket, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<FormEntity, FormTemplateModel>()
                .ReverseMap();

            CreateMap<SealConfigModel, SealConfigEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();
                
        }
    }
}
