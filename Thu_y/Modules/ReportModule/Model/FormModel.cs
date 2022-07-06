using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.ReportModule.Model
{
    public class FormModel
    {
        public string Id { get; set; }
        [Required]
        public string FormNumber { get; set; }
        [Required]
        public string FormName { get; set; }
        [Required]
        public string FormCode { get; set; }

        public ICollection<AttributeModel> Attributes { get; set; }
    }
}
