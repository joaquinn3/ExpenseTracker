using ExpenseTrackerWeb.Models;
using ExpenseTrackerWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExpenseTrackerWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            float balance = _unitOfWork.Transaction.GetAllWhere(x => x.category.Type == "Income").Sum(x => x.Amount);
            balance = balance - _unitOfWork.Transaction.GetAllWhere(x => x.category.Type == "Expense").Sum(x => x.Amount);
            if (balance >=0 )
            {
                ViewBag.Balance = balance.ToString("C2");
            }
            else
            {
                ViewBag.Balance = 0.ToString("C2");
            }


            float ExpensesMonth = _unitOfWork.Transaction.GetAllWhere(x => x.Date >= DateTime.Today.AddDays(-30) && x.Date <= DateTime.Today && x.category.Type=="Expense").Sum(x => x.Amount);
            ViewBag.ExpensesMonth = ExpensesMonth.ToString("C2");

            float IncomesMonth = _unitOfWork.Transaction.GetAllWhere(x => x.Date >= DateTime.Today.AddDays(-30) && x.Date <= DateTime.Today && x.category.Type=="Income").Sum(x => x.Amount);
            ViewBag.IncomesMonth = IncomesMonth.ToString("C2");


            List<Category> categories = _unitOfWork.Category.GetAllWhere(x => x.Type == "Expense").ToList();
            //List<Transaction> transactions = _unitOfWork.Transaction.GetAllWhere(x => x.category.Type == "Expense" && x.Date >= DateTime.Today.AddDays(-30) && x.Date <= DateTime.Today).ToList();
            List<ExpenseProgress> expenseProgress = new List<ExpenseProgress>();

            for (int i = 0; i < categories.Count; i++)
            {
                float categorySum = _unitOfWork.Transaction.GetAllWhere(x => x.category.Name == categories[i].Name && x.Date >= DateTime.Today.AddDays(-30) && x.Date <= DateTime.Today).Sum(x => x.Amount);
                expenseProgress.Add(new ExpenseProgress { Name = categories[i].NameWithIcon, Progress = (categorySum * 100) / ExpensesMonth, AmountFormat = categorySum.ToString("C2") });         
            }

            return View(expenseProgress);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
