﻿using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;

namespace Thu_y.Modules.AbttoirModule
{
    public class AbattoirDetailEntity : Entity
    {
        public int Status { get; set; }
        public int Amount { get; set; }
        public string AnimalId { get; set; }

        public string AbattoirId { get; set; }
        [ForeignKey("AbattoirId")]
        public virtual AbattoirEntity Abattoir { get; set; }
    }
}
