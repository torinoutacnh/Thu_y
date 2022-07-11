using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thu_y.Modules.ReportModule.Model
{
    public class ListQuarantineReportModel
    {
        public string ReportId { get; set; }

        public string ReportName { get; set; }

        public string STT { get; set; }

        public string OwnerName { get; set; }

        public string Address { get; set; }

        public string StartPlace { get; set; }

        public string EndPlace { get; set; }

        public string Quarantiner { get; set; }

        public int Amount { get; set; }
    }
}
