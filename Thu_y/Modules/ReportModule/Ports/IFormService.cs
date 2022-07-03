using Thu_y.Modules.ReportModule.Model;

namespace Thu_y.Modules.ReportModule.Ports
{
    public interface IFormService
    {
        bool CreateForm(FormModel model);
        bool UpdateForm(FormModel model);
    }
}
