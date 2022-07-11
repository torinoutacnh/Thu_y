using Thu_y.Infrastructure.Utils.Constant;

namespace Thu_y.Modules.ShareModule.Model
{
    public class AnimalModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DayAge { get; set; }
        public SexType Sex { get; set; }
    }
}
