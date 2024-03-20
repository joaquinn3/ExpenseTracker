using ExpenseTrackerWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerWeb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Categories = Set<Category>();
            Transactions = Set<Transaction>();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
