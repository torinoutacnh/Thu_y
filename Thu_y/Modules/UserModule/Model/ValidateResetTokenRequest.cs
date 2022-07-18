using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.UserModule.Model
{
    public class ValidateResetTokenRequest
    {
        [Required] public string Token { get; set; }
        [Required] public string Account { get; set; }
    }
}
