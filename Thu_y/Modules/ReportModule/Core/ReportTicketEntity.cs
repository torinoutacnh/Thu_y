﻿using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;
using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.ReportModule.Core
{
    [Table("ReportTicket")]
    public class ReportTicketEntity : Entity
    {
        public string? Name { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? FormId { get; set; }
        public ReportType Type { get; set; }

        public string? ApproveId { get; set; }
        public string? ApproveName { get; set; }
        public string? SerialNumber { get; set; }
        public string? NoteBook { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal TotalPrice { get; set; }

        public virtual ICollection<ReportTicketValueEntity>? Values { get; set; }

        public virtual ICollection<SealTabEntity>? SealTabs { get; set; }
        public virtual ICollection<ListAnimalEntity>? ListAnimals { get; set; }

    }
}
