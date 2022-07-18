using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.UserModule.Model
{
    public class ResendEmailModel
    {
        [Required]
        public string Account { get; set; }

    }
}
