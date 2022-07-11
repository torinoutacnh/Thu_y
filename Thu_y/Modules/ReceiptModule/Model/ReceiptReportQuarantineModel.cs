namespace Thu_y.Modules.ReceiptModule.Model
{
    public class ReceiptReportQuarantineModel
    {
        public string Id { get; set; }
        public string ReceiptName { get; set; }
        public DateTimeOffset DateUse { get; set; }
        public int PageUse { get; set; }
        public string UserName { get; set; }

    }
}
