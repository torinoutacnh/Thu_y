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
                .ReverseMap();

            CreateMap<AnimalEntity, AnimalModel>();
        }
    }
}
