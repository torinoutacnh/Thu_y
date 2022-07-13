namespace Thu_y.Modules.ReportModule.Model
{
    public class UpdateSealTabModel
    {
        public string ReportId { get; set; }
        public List<SealTabs> SealTabs {get; set;}
    }

    public class SealTabs
    {
        public string Id { get; set; }
        public string SealName { get; set; }
        public string SealCode { get; set; }
    }
}
