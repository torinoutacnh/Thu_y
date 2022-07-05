﻿using System.ComponentModel.DataAnnotations;

namespace Thu_y.Modules.ReportModule.Model
{
    public class ReportValueModel
    {
        public string Id { get; set; }
        [Required]
        public string AttributeId { get; set; }
        [Required]
        public string Value { get; set; }
        public string ReportId { get; set; }
    }
}