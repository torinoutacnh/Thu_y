using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;
using Thu_y.Modules.AbttoirModule;

namespace Thu_y.Modules.UserModule.Core
{
    public class UserScheduleEntity : Entity
    {
        public DateTimeOffset DateStart { get; set; }
        public DateTimeOffset DateEnd { get; set; }

        public string AbattoirId { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }
    }
}
