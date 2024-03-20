using ExpenseTrackerWeb.Models;

namespace ExpenseTrackerWeb.Repository.IRepository
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        void Update(Transaction obj);
    }
}
