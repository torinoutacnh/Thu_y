namespace Thu_y.Modules.ReportModule.Model
{
    public class UpdateListAnimalModel
    {
        public string ReportId { get; set; }
        public List<ListAnimals> ListAnimals { get; set; }
    }

    public class ListAnimals
    {
        public string Id { get; set; }
        public string AnimalName { get; set; }
        public string AnimalId { get; set; }
        public int DayAge { get; set; }
        public decimal Amount { get; set; }
    }
}
