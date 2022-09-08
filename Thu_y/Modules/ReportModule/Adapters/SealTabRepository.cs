using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.ReportModule.Core;
using Thu_y.Modules.ReportModule.Model;
using Thu_y.Modules.ReportModule.Ports;

namespace Thu_y.Modules.ReportModule.Adapters
{
    public class SealTabRepository : Repository<SealTabEntity>, ISealTabRepository
    {
        public SealTabRepository(IDbContext dbContext) : base(dbContext)
        {

        }

        public bool UpdateMultiSealTab (List<SealTabs> lsModel, string reportId)
        {
            StringBuilder sqlQuery = new StringBuilder(@$"UPDATE SealTab SET [Name] = CASE Id ");
            foreach (var seal in lsModel)
            {
                sqlQuery.Append($"WHEN '{seal.Id}' THEN N'{seal.SealName}' ");
            }

            sqlQuery.Append("ELSE [Name] END , [CodeSeal] = CASE [Id] ");
            foreach (var seal in lsModel)
            {
                sqlQuery.Append($"WHEN '{seal.Id}' THEN N'{seal.SealCode}' ");
            }
            sqlQuery.Append($"ELSE [CodeSeal] END, [Amount] = CASE [Id] ");
            foreach(var seal in lsModel)
            {
                sqlQuery.Append($"WHEN '{seal.Id}' THEN N'{seal.Amount}' ");
            }
            sqlQuery.Append($"ELSE [Amount] END WHERE ReportTicketId = @reportId");

            var param = new DynamicParameters();

            param.Add("reportId", reportId, DbType.String, ParameterDirection.Input);
            IDbConnection con = null;
            try
            {
                con = new SqlConnection(ConnectionString);
                con.Open();
                var result = con.Execute(sqlQuery.ToString(), param, commandType: CommandType.Text);
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
