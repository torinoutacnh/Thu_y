using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.UserModule.Model
{
    public class VerifyEmailRequestModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
