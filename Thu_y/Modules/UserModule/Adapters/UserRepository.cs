using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.Repository;
using Thu_y.Modules.UserModule.Core;
using Thu_y.Modules.UserModule.Ports;
using Thu_y.Utils.Infrastructure.Application;

namespace Thu_y.Modules.UserModule.Adapters
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public UserEntity GetSingle(Expression<Func<UserEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public UserEntity GetByVerifyToken(string token)
        {
            var sqlQuery = SqlGenerator.GetSelectFirst(_ => _.VerificationToken == token, null);
            IDbConnection con = null;
            try
            {
                con = new SqlConnection(ConnectionString);
                con.Open();
                var result = con.QuerySingle<UserEntity>(sqlQuery.GetSql(), sqlQuery.Param);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed) con.Close();
            }
        }

        public bool Edit(UserEntity entity)
        {
            entity.DateUpdated = SystemHelper.SystemTimeNow;
            var sqlQuery = SqlGenerator.GetUpdate(entity);
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
