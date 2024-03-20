using ExpenseTrackerWeb.Data;
using ExpenseTrackerWeb.Models;
using ExpenseTrackerWeb.Repository.IRepository;

namespace ExpenseTrackerWeb.Repository
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private readonly AppDbContext _appDbContext;
        public TransactionRepository(AppDbContext appDbContext) : base (appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Update(Transaction obj)
        {
            _appDbContext.Transactions.Update(obj);
        }
    }
}
