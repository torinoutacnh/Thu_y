﻿using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;

namespace Thu_y.Modules.ShareModule.Core
{
    public class VacineEntity : Entity
    {
        public string? Name { get; set; }
        public DateTimeOffset? DateInjected { get; set; }
        public string? VaccinationFacilityName { get; set; }
        public string? VaccinationFacilityAddress { get; set; }
        public string AnimalId { get; set; }
        [ForeignKey("AnimalId")]
        public virtual AnimalEntity? Animal { get; set; }
    }
}
