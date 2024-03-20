using ExpenseTrackerWeb.Models;

namespace ExpenseTrackerWeb.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
        bool Exists(Category obj);
    }
}
