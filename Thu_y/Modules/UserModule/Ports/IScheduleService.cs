using Thu_y.Modules.UserModule.Model;

namespace Thu_y.Modules.UserModule.Ports
{
    public interface IScheduleService
    {
        bool CreateSchedule(ScheduleModel model);
        bool UpdateSchedule(UpdateScheduleModel model);
        bool DeleteSchedule(string id);
    }
}
