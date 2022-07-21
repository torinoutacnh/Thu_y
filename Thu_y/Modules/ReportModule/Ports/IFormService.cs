using Thu_y.Modules.ReportModule.Model;

namespace Thu_y.Modules.ReportModule.Ports
{
    public interface IFormService
    {
        bool CreateForm(FormModel model);
        bool UpdateForm(FormModel model);
        bool DeleteForm(string id);
        ICollection<string> RecommentAttribute(string attributeName);
        FormModel GetSingleForm(string code, string refReportId);
    }
}
