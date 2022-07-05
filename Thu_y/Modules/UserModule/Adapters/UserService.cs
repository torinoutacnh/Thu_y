using AutoMapper;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.UserModule.Ports;
using Thu_y.Modules.UserModule.Core;
using Thu_y.Modules.UserModule.Model;

namespace Thu_y.Modules.UserModule.Adapters
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserScheduleRepository _userScheduleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IServiceProvider serviceProvider)
        {
            _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            _userScheduleRepository = serviceProvider.GetRequiredService<IUserScheduleRepository>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public bool CreateUser(UserModel model)
        {
            var user = _mapper.Map(model, new UserEntity());

            _userRepository.Add(user);
            _unitOfWork.SaveChange();

            return true;
        }

        public bool UpdateUser(UserModel model)
        {
            var user = _userRepository.Get(x => x.Id.Equals(model.Id)).FirstOrDefault();
            if (user == null) throw new Exception("No user found!") { HResult = 400 };

            var updated = _mapper.Map(model,user);
            _userRepository.Update(updated);
            _unitOfWork.SaveChange();

            return true;
        }

        public bool DeleteUser(string id)
        {
            var user = _userRepository.Get(x => x.Id.Equals(id)).FirstOrDefault();
            if (user == null) throw new Exception("No user found!") { HResult = 400 };

            _userRepository.Delete(user);
            _unitOfWork.SaveChange();

            return true;
        }
    }
}
