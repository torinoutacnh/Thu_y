using Thu_y.Modules.UserModule.Core;

namespace Thu_y.Modules.UserModule.Model
{
    public class ScheduleModel
    {
        public string Id { get; set; }

        public DateTimeOffset DateStart { get; set; }
        public DateTimeOffset DateEnd { get; set; }

        public string AbattoirId { get; set; }
        public string UserId { get; set; }
    }
}
