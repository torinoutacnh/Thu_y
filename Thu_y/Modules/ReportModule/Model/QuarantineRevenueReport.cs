namespace Thu_y.Modules.ReportModule.Model
{
    public class QuarantineRevenueReport
    {
        public string ReportId { get; set; }
        public string ReportName { get; set; }
        public string STT { get; set; }
        public string OwnerName { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int AnimalAmount { get; set; }
        public int SealAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public string Staff { get; set; }

    }
}
