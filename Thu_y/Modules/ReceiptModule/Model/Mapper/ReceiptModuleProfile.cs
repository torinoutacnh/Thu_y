using AutoMapper;
using Thu_y.Modules.ReceiptModule.Core;

namespace Thu_y.Modules.ReceiptModule.Model.Mapper
{
    public class ReceiptModuleProfile : Profile
    {
        public ReceiptModuleProfile()
        {
            CreateMap<ReceiptModel, ReceiptModel>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ReceiptAllocateModel, ReceiptAllocateEntity>()
                .ForMember(x=>x.Id, opt=>opt.Ignore())
                .ReverseMap();
        }
    }
}
