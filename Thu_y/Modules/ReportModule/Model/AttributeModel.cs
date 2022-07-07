using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.ReportModule.Model
{
    public class AttributeModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DataTypes DataType { get; set; }
        public ControlTypes ControlType { get; set; }
        public int SortNo { get; set; }
    }
}
