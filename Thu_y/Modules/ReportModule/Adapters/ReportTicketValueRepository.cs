using Dapper;
using System.Data;
using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.ReportModule.Core;
using Thu_y.Modules.ReportModule.Ports;
using Thu_y.Utils.Infrastructure.Application;

namespace Thu_y.Modules.ReportModule.Adapters
{
    public class ReportTicketValueRepository : Repository<ReportTicketValueEntity>, IReportTicketValueRepository
    {
        public ReportTicketValueRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public bool DeleteByAttributeId(string attributeId)
        {
            var dateTimeNow = SystemHelper.SystemTimeNow;

            var sqlQuery = SqlGenerator.GetDelete(_ => _.AttributeId == attributeId);
            IDbConnection con = null;
            try
            {
                con = new Microsoft.Data.SqlClient.SqlConnection(ConnectionString);
                con.Open();
                var result = con.Execute(sqlQuery.GetSql(), sqlQuery.Param);
                return result != 0;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed) con.Close();
            }
        }
    }
}
