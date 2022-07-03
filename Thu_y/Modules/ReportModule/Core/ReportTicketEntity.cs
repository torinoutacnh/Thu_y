using Thu_y.Infrastructure.Model;

namespace Thu_y.Modules.ReportModule.Core
{
    public class ReportTicketEntity : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<ReportTicketValueEntity> Values { get; set; }
    }
}
