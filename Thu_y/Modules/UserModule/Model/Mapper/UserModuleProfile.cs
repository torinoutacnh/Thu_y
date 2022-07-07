using AutoMapper;
using Thu_y.Modules.UserModule.Core;

namespace Thu_y.Modules.UserModule.Model.Mapper
{
    public class UserModuleProfile : Profile
    {
        public UserModuleProfile()
        {
            CreateMap<UserScheduleEntity, ScheduleModel>();
            CreateMap<UserScheduleEntity, UpdateScheduleModel>().ReverseMap();

            CreateMap<UserEntity, UserModel>();
            CreateMap<UserEntity, UserGetListModel>().ReverseMap();
            CreateMap<UserModel, UserEntity>()
                .ForMember(x=>x.Id,opt=>opt.Ignore());
        }
    }
}
