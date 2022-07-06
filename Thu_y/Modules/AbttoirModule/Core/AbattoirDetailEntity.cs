using System.ComponentModel.DataAnnotations.Schema;
using Thu_y.Infrastructure.Model;

namespace Thu_y.Modules.AbttoirModule.Core
{
    public class AbattoirDetailEntity : Entity
    {
        public int Status { get; set; }
        public int Amount { get; set; }
        public string AnimalId { get; set; }
    }
}
