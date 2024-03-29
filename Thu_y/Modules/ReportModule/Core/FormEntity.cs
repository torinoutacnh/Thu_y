﻿using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;

namespace Thu_y.Modules.ReportModule.Core
{
    [Table("Form")]
    public class FormEntity : Entity
    {
        public string? FormNumber { get; set; }
        public string? FormName { get; set; }
        public string? FormCode { get; set; }

        public virtual ICollection<FormAttributeEntity>? FormAttributes { get; set; }
    }
}
