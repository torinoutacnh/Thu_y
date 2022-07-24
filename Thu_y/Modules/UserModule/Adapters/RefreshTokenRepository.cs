using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.UserModule.Core;
using Thu_y.Modules.UserModule.Ports;

namespace Thu_y.Modules.UserModule.Adapters
{
    public class RefreshTokenRepository : Repository<RefreshToken> , IRefreshTokenRepository
    {
        public RefreshTokenRepository(IDbContext dbContext) : base(dbContext)
        { 
        }

        public bool Delete(RefreshToken entity)
        {
            var sqlQuery = SqlGenerator.GetDelete(entity);
            IDbConnection con = null;
            try
            {
                con = new SqlConnection(ConnectionString);
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
