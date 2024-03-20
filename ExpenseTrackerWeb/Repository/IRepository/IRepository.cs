using System.Linq.Expressions;

namespace ExpenseTrackerWeb.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProperties = null);
        T GetOne(Expression<Func<T, bool>> filter);
        void Create(T entity);
        void Remove(T entity);
        IEnumerable<T> GetAllWhere(Expression<Func<T, bool>> filter);
    }
}
