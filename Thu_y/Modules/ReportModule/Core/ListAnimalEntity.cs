﻿using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;
using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.ReportModule.Core
{
    public class ListAnimalEntity : Entity
    {
        public string AnimalName { get; set; }
        public string AnimalId { get; set; }
        public bool IsCar { get; set; }
        public decimal Amount { get; set; }
        public SexType AnimalSex { get; set; }
        public string Purpose { get; set; }
        public string ReportTicketId { get; set; }

        [ForeignKey("ReportTicketId")]
        public virtual ReportTicketEntity ReportTicket { get; set; }

    }
}