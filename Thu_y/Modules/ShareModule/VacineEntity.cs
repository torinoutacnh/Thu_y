using Thu_y.Infrastructure.Model;

namespace Thu_y.Modules.ShareModule
{
    public class VacineEntity : Entity
    {
        public string Name { get; set; }
        public DateTimeOffset DateInjected { get; set; }
        public string VaccinationFacilityName { get; set; }
        public string VaccinationFacilityAddress { get; set; }
    }
}
