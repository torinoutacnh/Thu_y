using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.UserModule.Model
{
    public class ForgotPasswordRequestModel
    {
        [Required]
        public string Account { get; set; }
    }
}
