using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.ReportModule.Model
{
    public class SealTabModel
    {
        public string Id { get; set; }
        public string CodeSeal { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Id_Pricing { get; set; }
    }
}
