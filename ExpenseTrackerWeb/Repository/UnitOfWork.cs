using ExpenseTrackerWeb.Data;
using ExpenseTrackerWeb.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerWeb.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }
        public ITransactionRepository Transaction { get; private set; }


        private readonly AppDbContext _appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            Category = new CategoryRepository(appDbContext);
            Transaction = new TransactionRepository(appDbContext);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
