using AutoMapper;
using Thu_y.Modules.ShareModule.Core;

namespace Thu_y.Modules.ShareModule.Model.Mapper
{
    public class ShareModuleProfile : Profile
    {
        public ShareModuleProfile()
        {
            CreateMap<AnimalModel, AnimalEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(des => des.Vacines, opt=> opt.MapFrom((src,des) =>src.Vacines))
                .ReverseMap();

            CreateMap<AnimalEntity, AnimalModel>();
        }
    }
}
