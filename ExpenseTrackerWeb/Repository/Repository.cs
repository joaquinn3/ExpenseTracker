using ExpenseTrackerWeb.Data;
using ExpenseTrackerWeb.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace ExpenseTrackerWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> dbSet;
        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            dbSet = _appDbContext.Set<T>();
        }
        public void Create(T entity)
        {
            dbSet.Add(entity);

        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {

            if (!string.IsNullOrEmpty(includeProperties))
            {
                IQueryable<T> query = dbSet;
                query = query.Include(includeProperties);
                return query.ToList();
            }


            return dbSet.ToList();
        }

        public IEnumerable<T> GetAllWhere(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            return query.ToList();
        }

        public T GetOne(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
