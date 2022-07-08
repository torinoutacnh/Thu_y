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
    public class ReportTicketRepository : Repository<ReportTicketEntity>, IReportTicketRepository
    {
        public ReportTicketRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public ICollection<AnimalKillingReportModel> GetAnimalKillingReport(string userId, string? reportName = null)
        {
            var queryDetail = string.IsNullOrEmpty(reportName)? "" : $"and [Name] = '{reportName}'";
            StringBuilder sqlQuery = new StringBuilder(@$"select ReportName,
                                                            [Tổng số được kiểm tra lâm sàng] as [Total],
	                                                        [Số lượng tồn ngày hôm trước] as [Inventory],
	                                                        [Số lượng giết mổ] as [Killed],
	                                                        [Số lượng, lý do chưa giết mổ] as [Survival]
                                                        from(
                                                        SELECT * FROM
                                                        (
	                                                        select rp.[Name] as ReportName, rv.AttributeName, cast(rv.[Value] as decimal(12,2)) as [value], rv.ReportId 
                                                            from ReportTicketValue rv inner join ReportTicket rp on rp.Id = rv.ReportId 
	                                                        where rv.ReportId in (select  Id from ReportTicket where UserId = @userId {queryDetail})
                                                        ) t
                                                        PIVOT(
                                                        	sum([value])
                                                        	for AttributeName in(
                                                        		[Tổng số được kiểm tra lâm sàng],
                                                        		[Số lượng tồn ngày hôm trước],
                                                        		[Số lượng giết mổ],
                                                        		[Số lượng, lý do chưa giết mổ])
                                                        ) as pivot_table) as tb");

            var param = new DynamicParameters();
            param.Add("userId", userId, DbType.String, ParameterDirection.Input);
            IDbConnection con = null;
            try
            {
                con = new SqlConnection(ConnectionString);
                con.Open();
                var result = con.Query<AnimalKillingReportModel>(sqlQuery.ToString(), param).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return new List<AnimalKillingReportModel>();
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed) con.Close();
            }
        }

        public ICollection<ListAbttoirReportModel> GetListAbattoirReport(string userId)
        {
            StringBuilder sqlQuery = new StringBuilder(@$"select ReportName,
                                                            [Tổng số được kiểm tra lâm sàng] as [Total],
	                                                        [Số lượng, lý do chưa giết mổ] as [Survival]
                                                        from(
                                                        SELECT * FROM
                                                        (
	                                                        select rp.[Name] as ReportName, rv.AttributeName, cast(rv.[Value] as decimal(12,2)) as [value] 
                                                            from ReportTicketValue rv inner join ReportTicket rp on rp.Id = rv.ReportId 
	                                                        where rv.ReportId in (select  Id from ReportTicket where UserId = @userId)
                                                        ) t
                                                        PIVOT(
                                                        	sum([value])
                                                        	for AttributeName in(
                                                        		[Tổng số được kiểm tra lâm sàng],
                                                        		[Số lượng tồn ngày hôm trước],
                                                        		[Số lượng giết mổ],
                                                        		[Số lượng, lý do chưa giết mổ])
                                                        ) as pivot_table) as tb");

            var param = new DynamicParameters();
            param.Add("userId", userId, DbType.String, ParameterDirection.Input);
            IDbConnection con = null;
            try
            {
                con = new SqlConnection(ConnectionString);
                con.Open();
                var result = con.Query<ListAbttoirReportModel>(sqlQuery.ToString(), param).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return new List<ListAbttoirReportModel>();
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed) con.Close();
            }
        }
    }
}
