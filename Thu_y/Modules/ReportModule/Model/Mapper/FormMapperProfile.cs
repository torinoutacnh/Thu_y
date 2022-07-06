using AutoMapper;
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
                .ReverseMap();

            CreateMap<ReportValueModel, ReportTicketValueEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
