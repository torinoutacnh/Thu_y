using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.UserModule.Model
{
    public class UpdateScheduleModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public DateTimeOffset DateStart { get; set; }
        [Required]
        public DateTimeOffset DateEnd { get; set; }
    }
}
