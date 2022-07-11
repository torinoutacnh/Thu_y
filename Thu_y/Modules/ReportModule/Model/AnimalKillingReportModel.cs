using System.ComponentModel.DataAnnotations.Schema;

namespace Thu_y.Modules.ReportModule.Model
{
    public class AnimalKillingReportModel
    {
        public string ReportId { get; set; }
        public string ReportName { get; set; }
        public string STT { get; set; }
        public string AbattoirOwner { get; set; }
        public string MedicalStaff { get; set; }
        public DateTimeOffset Time { get; set; }


        [Column(TypeName = "decimal(12, 2)")]
        public decimal Total { get; set; }

        [Column(TypeName = "decimal(12, 2)")]
        public decimal Dead { get; set; }

        [Column(TypeName = "decimal(12, 2)")]
        public decimal Alive { get; set; }
    }
}
