using AutoMapper;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.UserModule.Core;
using Thu_y.Modules.UserModule.Model;
using Thu_y.Modules.UserModule.Ports;

namespace Thu_y.Modules.UserModule.Adapters
{
    public class ScheduleService : IScheduleService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserScheduleRepository _userScheduleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ScheduleService(IServiceProvider serviceProvider)
        {
            _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            _userScheduleRepository = serviceProvider.GetRequiredService<IUserScheduleRepository>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public bool CreateSchedule(ScheduleModel model)
        {
            if (_userRepository.Get(x => x.Id.Equals(model.UserId)).Any())
            {
                throw new Exception("User not exist!") { HResult = 404 };
            }

            var schedule = _mapper.Map<UserScheduleEntity>(model);
            _userScheduleRepository.Add(schedule);
            _unitOfWork.SaveChange();

            return true;
        }

        public bool UpdateSchedule(UpdateScheduleModel model)
        {
            var old = _userScheduleRepository.Get(x => x.Id.Equals(model.Id)).FirstOrDefault();
            if (old == null)
            {
                throw new Exception("Schedule not exist!") { HResult = 404 };
            }

            var schedule = _mapper.Map(model, old);

            _userScheduleRepository.Update(schedule);
            _unitOfWork.SaveChange();

            return true;
        }

        public bool DeleteSchedule(string id)
        {
            var old = _userScheduleRepository.Get(x => x.Id.Equals(id)).FirstOrDefault();
            if (old == null)
            {
                throw new Exception("Schedule not exist!") { HResult = 404 };
            }
            _userScheduleRepository.Delete(old);
            _unitOfWork.SaveChange();

            return true;
        }
    }
}
