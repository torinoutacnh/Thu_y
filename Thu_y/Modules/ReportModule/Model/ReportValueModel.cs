using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.ReportModule.Model
{
    public class ReportValueModel
    {
        public string Id { get; set; }
        //[Required]
        public string AttributeId { get; set; }

        public string Value { get; set; }

        //[Required]
        public string ReportId { get; set; }

        //[Required]
        public string AttributeName { get; set; }
        public string AttributeCode { get; set; }
        public string FormName { get; set; }
        public int Sort { get; set; }    }
}
