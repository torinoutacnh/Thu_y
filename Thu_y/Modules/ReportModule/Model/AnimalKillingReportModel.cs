using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

    public class AnimalDailyReportModel
    {
        public string STT { get; set; }
        [DisplayName("Tên chủ động vật hoặc chủ hàng")]
        public string Owner { get; set; }
        [DisplayName("Loại Động vật")]
        public string Animal { get; set; }
        [DisplayName("Số lượng cùng một lô")]
        public string PackSize { get; set; }
        [DisplayName("Thời gian nhập(ngày)")]
        public DateTimeOffset DateReceived { get; set; }
        [DisplayName("Số lượng tồn ngày hôm trước")]
        [Column(TypeName = "decimal(12, 2)")]
        public decimal YesterdayLeftover { get; set; }
        [DisplayName("Tổng số được kiểm tra lâm sàng")]
        [Column(TypeName = "decimal(12, 2)")]
        public decimal CheckedAmount { get; set; }
        [DisplayName("Kết quả kiểm tra trước giết mổ")]
        public string CheckedResult { get; set; }
        [DisplayName("Số lượng giết mổ")]
        [Column(TypeName = "decimal(12, 2)")]
        public decimal ProcessedAmount { get; set; }
        [DisplayName("Số lượng, lý do chưa giết mổ")]
        [Column(TypeName = "decimal(12, 2)")]
        public decimal NotProcessedAmount { get; set; }
        [DisplayName("Biện pháp sử lý")]
        public string Solution { get; set; }
        [DisplayName("Chữ ký của chủ cơ sở")]
        public string OwnerSign { get; set; }
        [DisplayName("Chữ ký của nhân viên thú y")]
        public string QuarantinerSign { get; set; }
    }
}
