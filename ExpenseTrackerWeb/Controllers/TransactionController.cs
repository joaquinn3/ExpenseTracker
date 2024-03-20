using ExpenseTrackerWeb.Data;
using ExpenseTrackerWeb.Models;
using ExpenseTrackerWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerWeb.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Transaction> transactions = _unitOfWork.Transaction.GetAll("category").ToList();
            return View(transactions);
        }

        public IActionResult Create()
        {
            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            ViewBag.Categories = categories;
            return View(new Transaction());
        }

        [HttpPost]
        public IActionResult Create(Transaction obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            if (obj.CategoryId==0)
            {
                ModelState.AddModelError("CategoryId", "Category is required.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Transaction.Create(obj);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            ViewBag.Categories = categories;
            return View();
        }

        public IActionResult Edit(int? id)
        {

            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Transaction obj = _unitOfWork.Transaction.GetOne(x => x.TransactionId == id);

            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            ViewBag.Categories = categories;
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(Transaction? obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            if (obj.CategoryId == 0)
            {
                ModelState.AddModelError("CategoryId", "Category is required.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Transaction.Update(obj);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }


            Transaction obj = _unitOfWork.Transaction.GetOne(x => x.TransactionId == id);


            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            ViewBag.Categories = categories;

            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Transaction obj)
        {
            _unitOfWork.Transaction.Remove(obj);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult CustomTransaction(string? categoryName)
        {
            if (categoryName == null)
            {
                return NotFound();
            }

            List<Transaction> transactions = _unitOfWork.Transaction.GetAllWhere(x => x.category.Name == categoryName).ToList();

            return View(transactions);
        }

    }
}
