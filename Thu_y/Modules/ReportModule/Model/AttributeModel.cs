﻿using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.ReportModule.Model
{
    public class AttributeModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DataTypes DataType { get; set; }
        public ControlTypes ControlType { get; set; }
        public string? api_DropDownlist { get; set; }
        public int SortNo { get; set; }
        public string AttributeCode { get; set; }
        public string Value { get; set; }
        public string? AttributeGroup { get; set; }
    }
}
