using AutoMapper;
using Thu_y.Modules.ReceiptModule.Core;

namespace Thu_y.Modules.ReceiptModule.Model.Mapper
{
    public class ReceiptModuleProfile : Profile
    {
        public ReceiptModuleProfile()
        {
            CreateMap<ReceiptModel, ReceiptEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Allocates, opt => opt.MapFrom(y => y.Allocates))
                .ReverseMap();

            CreateMap<ReceiptAllocateModel, ReceiptAllocateEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.TotalPage, opt => opt.Ignore())
                .ForMember(x => x.RemainPage, opt => opt.Ignore());

            CreateMap<ReceiptAllocateEntity, ReceiptAllocateModel>();

            CreateMap<ReceiptReportModel, ReceiptReportEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<ReceiptReportEntity, ReceiptReportModel>();

            CreateMap<ReceiptReportEntity, ReceiptReportQuarantineModel>().ReverseMap();

            CreateMap<UpdateReceiptModel, ReceiptEntity>();
        }
    }
}
