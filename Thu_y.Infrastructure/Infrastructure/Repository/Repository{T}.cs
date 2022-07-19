using Dapper;
using Invedia.Data.Dapper.SqlGenerator;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using Thu_y.Infrastructure.DbContext;
using Thu_y.Utils.Infrastructure.Application;

namespace Thu_y.Infrastructure.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Model.Entity, new()
    {
        protected readonly IDbContext DbContext;
        protected ISqlGenerator<T> SqlGenerator { get; }
        protected string ConnectionString;

        private DbSet<T> _dbSet;

        protected DbSet<T> DbSet
        {
            get
            {
                if (_dbSet != null)
                {
                    return _dbSet;
                }

                _dbSet = DbContext.Set<T>();
                return _dbSet;
            }
        }

        protected Repository(IDbContext dbContext)
        {
            DbContext = dbContext;
            ConnectionString = SystemHelper.AppDb;
            SqlGenerator = new SqlGenerator<T>(SqlProvider.MSSQL, true);
        }

        protected void TryAttach(T entity)
        {
            try
            {
                if (DbContext.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
            }
            catch
            {
            }
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>>? predicate = null, bool isIncludeDeleted = false, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> source = DbSet.AsNoTracking();
            if (predicate != null)
            {
                source = source.Where(predicate);
            }

            includeProperties = includeProperties?.Distinct().ToArray();
            if (includeProperties?.Any() ?? false)
            {
                Expression<Func<T, object>>[] array = includeProperties;
                foreach (Expression<Func<T, object>> navigationPropertyPath in array)
                {
                    source = source.Include(navigationPropertyPath);
                }
            }

            return isIncludeDeleted ? source.IgnoreQueryFilters() : source.Where((T x) => x.DateDeleted == null);
        }

        public T Add(T entity)
        {
            entity.DateDeleted = null;
            entity.DateUpdated = entity.DateCreated = DateTimeOffset.UtcNow;
            entity = DbSet.Add(entity).Entity;
            return entity;
        }

        public virtual List<T> AddRange(params T[] entities)
        {
            DateTimeOffset utcNow = DateTimeOffset.UtcNow;
            List<T> list = new List<T>();
            foreach (T val in entities)
            {
                val.DateCreated = utcNow;
                T item = Add(val);
                list.Add(item);
            }

            return list;
        }

        public void Update(T entity, params Expression<Func<T, object>>[] changedProperties)
        {
            TryAttach(entity);
            changedProperties = changedProperties?.Distinct().ToArray();
            entity.DateUpdated = DateTimeOffset.UtcNow;
            if (changedProperties?.Any() ?? false)
            {
                DbContext.Entry(entity).Property((T x) => x.DateUpdated).IsModified = true;
                Expression<Func<T, object>>[] array = changedProperties;
                foreach (Expression<Func<T, object>> propertyExpression in array)
                {
                    DbContext.Entry(entity).Property(propertyExpression).IsModified = true;
                }
            }
            else
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Update(T entity, params string[] changedProperties)
        {
            TryAttach(entity);
            changedProperties = changedProperties?.Distinct().ToArray();
            entity.DateUpdated = DateTimeOffset.UtcNow;
            if (changedProperties?.Any() ?? false)
            {
                DbContext.Entry(entity).Property((T x) => x.DateUpdated).IsModified = true;
                string[] array = changedProperties;
                foreach (string propertyName in array)
                {
                    DbContext.Entry(entity).Property(propertyName).IsModified = true;
                }
            }
            else
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Update(T entity)
        {
            TryAttach(entity);
            entity.DateUpdated = DateTimeOffset.UtcNow;
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity, bool isPhysicalDelete = false)
        {
            try
            {
                TryAttach(entity);
                if (!isPhysicalDelete)
                {
                    entity.DateDeleted = DateTimeOffset.UtcNow;
                    DbContext.Entry(entity).Property((T x) => x.DateDeleted).IsModified = true;
                }
                else
                {
                    DbSet.Remove(entity);
                }
            }
            catch (Exception)
            {
                RefreshEntity(entity);
                throw;
            }
        }

        public virtual void RefreshEntity(T entity)
        {
            DbContext.Entry(entity).Reload();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public T GetSingle(Expression<Func<T, bool>>? predicate = null, bool isIncludeDeleted = false, params Expression<Func<T, object>>[] includeProperties)
        {
            
            return Get(predicate, isIncludeDeleted, includeProperties).FirstOrDefault();
        }

        public bool Insert(T entity)
        {
            var dateTimeNow = SystemHelper.SystemTimeNow;
            entity.DateCreated= dateTimeNow;
            entity.DateUpdated = dateTimeNow;

            var sqlQuery = SqlGenerator.GetInsert(entity);
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

        public T GetByKey(object key)
        {
            return DbSet.Find(key);
        }
    }
}
