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

        public ICollection<AnimalKillingReportModel> GetAnimalKillingReport(string userId)
        {
            //var queryDetail = string.IsNullOrEmpty(reportName)? "" : $"and [Name] = '{reportName}'";
            StringBuilder sqlQuery = new StringBuilder(@$"select ReportName, ReportId,
                                                            [Số] as [STT],
	                                                        [Chữ ký của chủ cơ sở] as [AbattoirOwner],
	                                                        [Chữ ký của nhân viên thú y] as [MedicalStaff],
	                                                        cast([Thời gian nhập(ngày)] as datetimeoffset(7)) as [Time],
															isnull(cast([Số lượng cùng một lô] as decimal(12,2)), 0) as [Total],
															isnull(cast([Số lượng giết mổ] as decimal(12,2)), 0) as [Dead],
															isnull(cast([Số lượng tồn ngày hôm trước] as decimal(12,2)), 0) as [Alive]
                                                        from(
                                                        SELECT * FROM
                                                        (
	                                                        select rp.[Name] as ReportName, rv.AttributeName, rv.[Value] as [value], rv.ReportId 
                                                            from ReportTicketValue rv inner join ReportTicket rp on rp.Id = rv.ReportId 
	                                                        where rv.ReportId in (select  Id from ReportTicket where UserId = @userId )
															and rp.FormId = 'c81c3aad-b1f1-41de-80c7-ee1f724b6a1d'
                                                            and rp.[DateDeleted] is null
                                                        ) t
                                                        PIVOT(
                                                        	max([value])
                                                        	for AttributeName in(
                                                        		[Số],
                                                        		[Chữ ký của chủ cơ sở],
                                                        		[Chữ ký của nhân viên thú y],
                                                        		[Thời gian nhập(ngày)],
																[Số lượng cùng một lô],
																[Số lượng giết mổ],
																[Số lượng tồn ngày hôm trước])
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
                                                            and rp.[DateDeleted] is null
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

        public ICollection<QuarantineRevenueReport> GetQuarantineRevenueReport(DateTimeOffset fromDay, DateTimeOffset toDay) // Báo cáo doanh thu kiểm dịch
        {
            StringBuilder sqlQuery = new StringBuilder(@$"select ReportName, ReportId, DateCreated, AnimalAmount, SealAmount, TotalPrice,
														[Số] as [STT],
														[Chữ ký của chủ cơ sở] as [OwnerName],
														[Tên người kiểm dịch] as [Staff]
                                                        from(
                                                        SELECT * FROM
                                                        (
	                                                        select rp.[Name] as ReportName, rv.AttributeName, rv.[Value] as [value], rv.ReportId,rp.DateCreated,
															(select count(*) from ListAnimal where rp.Id = ReportTicketId) as [AnimalAmount],
															(select count(*) from SealTab where rp.Id = ReportTicketId) as [SealAmount],
															isnull(rp.TotalPrice +(select sum(Price) from SealTab where rp.Id = ReportTicketId),0) as [TotalPrice]
                                                            from ReportTicketValue rv inner join ReportTicket rp on rp.Id = rv.ReportId 
	                                                        where rv.ReportId in (select  Id from ReportTicket 
																				  where convert(varchar(10),DateCreated,110)>= convert(varchar(10),@fromDay,110)
																				  and convert(varchar(10),DateCreated,110)<= convert(varchar(10),@toDay,110))
															and rp.FormId = '4e64e271-38f9-4f87-9c7a-c03df9fa67fb'
                                                            and rp.[DateDeleted] is null
                                                        ) t
                                                        PIVOT(
                                                        	max([value])
                                                        	for AttributeName in(
                                                        		[Số],
                                                        		[Chữ ký của chủ cơ sở],
																[Tên người kiểm dịch])
                                                        ) as pivot_table) as tb");

            var param = new DynamicParameters();
            param.Add("@fromDay", fromDay, DbType.DateTimeOffset, ParameterDirection.Input);
            param.Add("@toDay", toDay, DbType.DateTimeOffset, ParameterDirection.Input);
            IDbConnection con = null;
            try
            {
                con = new SqlConnection(ConnectionString);
                con.Open();
                var result = con.Query<QuarantineRevenueReport>(sqlQuery.ToString(), param).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return new List<QuarantineRevenueReport>();
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed) con.Close();
            }
        }

        public ICollection<ListQuarantineReportModel> GetListQuarantineReport(string userId) //danh sách báo cáo kiểm dịch
        {
            StringBuilder sqlQuery = new StringBuilder(@$"select ReportName, ReportId,
															[Số] as [STT],
	                                                        [Họ và tên chủ hàng] as [OwnerName],
                                                            [Địa chỉ giao dịch] as [Address],
                                                            [Nơi xuất phát] as [StartPlace],
                                                            [Nơi đến cuối cùng] as [EndPlace],
                                                            [Tên người kiểm dịch] as [Quarantiner]
                                                        from(
                                                        SELECT * FROM
                                                        (
	                                                        select rp.[Name] as ReportName, rp.Id as ReportId, rv.AttributeName, rv.[Value] as [value]
															,(select count(*) from ListAnimal where rp.Id = ReportTicketId) as [Amount]
                                                            from ReportTicketValue rv inner join ReportTicket rp on rp.Id = rv.ReportId
	                                                        where rv.ReportId in (select  Id from ReportTicket where UserId = @userId)
                                                            and rp.FormId = '4e64e271-38f9-4f87-9c7a-c03df9fa67fb'
                                                            and rp.[DateDeleted] is null
                                                        ) t
                                                        PIVOT(
                                                        	max([value])
                                                        	for AttributeName in(
                                                        		[Số] ,
																[Họ và tên chủ hàng],
                                                        		[Địa chỉ giao dịch],
                                                        		[Nơi xuất phát],
                                                        		[Nơi đến cuối cùng],
																[Tên người kiểm dịch]
																)
                                                        ) as pivot_table
														) as tb");

            var param = new DynamicParameters();
            param.Add("userId", userId, DbType.String, ParameterDirection.Input);
            IDbConnection con = null;
            try
            {
                con = new SqlConnection(ConnectionString);
                con.Open();
                var result = con.Query<ListQuarantineReportModel>(sqlQuery.ToString(), param).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return new List<ListQuarantineReportModel>();
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed) con.Close();
            }
        }

        public bool UpdateMultiReport(List<Values> lsModel, string reportId)
        {
            StringBuilder sqlQuery = new StringBuilder(@$"UPDATE [ReportTicketValue] SET [Value] = CASE [AttributeId] ");
            foreach (var data in lsModel)
            {
                sqlQuery.Append($"WHEN '{data.AttributeId}' THEN '{data.Value}' ");
            }

            sqlQuery.Append("ELSE [Value] END WHERE [ReportId] = @reportId ");

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
