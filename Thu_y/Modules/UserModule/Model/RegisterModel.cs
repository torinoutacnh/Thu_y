using System.ComponentModel.DataAnnotations;
using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.UserModule.Model
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Account { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public SexType Sex { get; set; }
        public RoleType Role { get; set; }
    }
}
