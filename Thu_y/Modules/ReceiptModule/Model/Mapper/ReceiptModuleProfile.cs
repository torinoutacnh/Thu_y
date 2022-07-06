using AutoMapper;
using Thu_y.Modules.ReceiptModule.Core;

namespace Thu_y.Modules.ReceiptModule.Model.Mapper
{
    public class ReceiptModuleProfile : Profile
    {
        public ReceiptModuleProfile()
        {
            CreateMap<ReceiptEntity, ReceiptModel>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<ReceiptEntity, ReceiptModel>();

            CreateMap<ReceiptAllocateEntity, ReceiptAllocateModel>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<ReceiptAllocateModel, ReceiptAllocateEntity>();
        }
    }
}
