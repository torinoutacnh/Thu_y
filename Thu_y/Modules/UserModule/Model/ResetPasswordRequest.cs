using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.UserModule.Model
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Account { get; set; }
    }
}
