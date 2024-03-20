using ExpenseTrackerWeb.Data;
using ExpenseTrackerWeb.Models;
using ExpenseTrackerWeb.Repository.IRepository;

namespace ExpenseTrackerWeb.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public CategoryRepository(AppDbContext appDbContext) : base (appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Update(Category obj)
        {
            _appDbContext.Categories.Update(obj);
        }

        public bool Exists(Category obj)
        {
            Category? sameName = _appDbContext.Categories.Where(x => x.CategoryId != obj.CategoryId && x.Name == obj.Name.ToLower().Trim()).FirstOrDefault();

            if (sameName != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
