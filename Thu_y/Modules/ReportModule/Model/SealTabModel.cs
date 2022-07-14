using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.ReportModule.Model
{
    public class SealTabModel
    {
        public string Id { get; set; }

        [Required]
        public string SealCode { get; set; }

        [Required]
        public string SealName { get; set; }
        public string Content { get; set; }
        public string Id_Pricing { get; set; }
        public string ReportTicketId { get; set; }
        public decimal Price { get; set; }
    }
}
