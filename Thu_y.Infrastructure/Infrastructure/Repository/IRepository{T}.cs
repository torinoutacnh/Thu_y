using System.Linq.Expressions;

namespace Thu_y.Infrastructure.Repository
{
    public interface IRepository<T> : IDisposable where T : Model.Entity, new()
    {
        IQueryable<T> Get(Expression<Func<T, bool>>? predicate = null, bool isIncludeDeleted = false, params Expression<Func<T, object>>[] includeProperties);
        T Add(T entity);
        List<T> AddRange(params T[] entities);
        void Update(T entity, params Expression<Func<T, object>>[] changedProperties);
        void Update(T entity, params string[] changedProperties);
        void Update(T entity);
        void Delete(T entity, bool isPhysicalDelete = false);
        T GetSingle(Expression<Func<T, bool>>? predicate = null, bool isIncludeDeleted = false, params Expression<Func<T, object>>[] includeProperties);
        bool Insert(T entity);
    }
}
