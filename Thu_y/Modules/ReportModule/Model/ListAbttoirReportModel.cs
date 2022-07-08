using System.ComponentModel.DataAnnotations.Schema;

namespace Thu_y.Modules.ReportModule.Model
{
    public class ListAbttoirReportModel
    {
        public string ReportName { get; set; }

        [Column(TypeName = "decimal(12, 2)")]
        public decimal Total { get; set; }

        [Column(TypeName = "decimal(12, 2)")]
        public decimal Survival { get; set; }

    }
}
