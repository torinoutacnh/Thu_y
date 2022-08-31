namespace Thu_y.Modules.ReportModule.Model
{
    public class UpdateReportModel
    {
        public string ReportId { get; set; }
        public string? SerialNumber { get; set; }
        public List<Values> Values { get; set; }
    }

    public class Values
    {
        public string AttributeId { get; set; }
        public string Value { get; set; }

    }
}
