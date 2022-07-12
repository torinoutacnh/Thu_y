using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.ReportModule.Model
{
    public class ListAnimalModel
    {
        public string Id { get; set; }
        public string AnimalName { get; set; }
        public string AnimalId { get; set; }
        public bool IsCar { get; set; }
        public decimal Amount { get; set; }
        public int? DayAge { get; set; }
        public SexType AnimalSex { get; set; }
    }
}
