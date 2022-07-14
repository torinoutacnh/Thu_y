using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.ReportModule.Core;
using Thu_y.Modules.ReportModule.Model;

namespace Thu_y.Modules.ReportModule.Ports
{
    public interface IListAnimalRepository : IRepository<ListAnimalEntity>
    {
        bool UpdateMultiListAnimal(List<ListAnimals> lsModel, string reportId);
    }
}
