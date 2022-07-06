using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.AbttoirModule.Model
{
    public class AbattoirModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ManagerName { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
