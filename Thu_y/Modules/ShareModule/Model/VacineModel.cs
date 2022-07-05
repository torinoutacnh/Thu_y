namespace Thu_y.Modules.ShareModule.Model
{
    public class VacineModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DateInjected { get; set; }
        public string VaccinationFacilityName { get; set; }
        public string VaccinationFacilityAddress { get; set; }
    }
}
