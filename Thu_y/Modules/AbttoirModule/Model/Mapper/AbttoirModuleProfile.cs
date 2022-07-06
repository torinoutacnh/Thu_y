using AutoMapper;
using Thu_y.Modules.AbttoirModule.Core;

namespace Thu_y.Modules.AbttoirModule.Model.Mapper
{
    public class AbttoirModuleProfile : Profile
    {
        public AbttoirModuleProfile()
        {
            CreateMap<AbattoirModel, AbattoirEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<AbattoirEntity, AbattoirModel>();
        }
    }
}
