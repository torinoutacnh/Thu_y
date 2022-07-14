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
    public class ListAnimalRepository : Repository<ListAnimalEntity>, IListAnimalRepository
    {
        public ListAnimalRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public bool UpdateMultiListAnimal(List<ListAnimals> lsModel, string reportId)
        {
            StringBuilder sqlQuery = new StringBuilder(@$"UPDATE [ListAnimal] SET [AnimalName] = CASE Id ");
            foreach (var ani in lsModel)
            {
                sqlQuery.Append($"WHEN '{ani.Id}' THEN N'{ani.AnimalName}' ");
            }

            sqlQuery.Append("ELSE [AnimalName] END , [DayAge] = CASE [Id] ");

            foreach (var ani in lsModel)
            {
                sqlQuery.Append($"WHEN '{ani.Id}' THEN {ani.DayAge} ");
            }

            sqlQuery.Append("ELSE [DayAge] END , [Amount] = CASE [Id] ");

            foreach (var ani in lsModel)
            {
                sqlQuery.Append($"WHEN '{ani.Id}' THEN {ani.Amount} ");
            }

            sqlQuery.Append("ELSE [Amount] END , [TotalPrice] = CASE [Id] ");

            foreach (var ani in lsModel)
            {
                sqlQuery.Append($"WHEN '{ani.Id}' THEN {ani.Amount} * isnull((select Pricing from [Animal] where [Name] = '{ani.AnimalName}'),0) ");
            }

            sqlQuery.Append("ELSE [TotalPrice] END , [AnimalId] = CASE [Id] ");

            foreach (var ani in lsModel)
            {
                sqlQuery.Append($"WHEN '{ani.Id}' THEN N'{ani.AnimalId}' ");
            }

            sqlQuery.Append($"ELSE [AnimalId] END WHERE ReportTicketId = @reportId");

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
