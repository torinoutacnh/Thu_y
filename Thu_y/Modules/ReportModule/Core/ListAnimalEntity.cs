using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;
using Thu_y.Infrastructure.Utils.Constant;
namespace Thu_y.Modules.ReportModule.Core
{
    [Table("ListAnimal")]
    public class ListAnimalEntity : Entity
    {
        public string? AnimalName { get; set; }
        public string? AnimalId { get; set; }
        public bool IsCar { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Amount { get; set; }
        public int? DayAge { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal TotalPrice { get; set; } // dựa vào animalentity để lấy đơn giá nhân với Amount = total
        public SexType AnimalSex { get; set; }
        public string? Purpose { get; set; }
        public string ReportTicketId { get; set; }

        [ForeignKey("ReportTicketId")]
        public virtual ReportTicketEntity? ReportTicket { get; set; }

    }
}
