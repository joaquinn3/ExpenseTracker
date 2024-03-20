namespace ExpenseTrackerWeb.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ITransactionRepository Transaction { get; }
        void Save();
    }
}
