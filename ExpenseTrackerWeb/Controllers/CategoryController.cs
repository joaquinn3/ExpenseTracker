using ExpenseTrackerWeb.Models;
using ExpenseTrackerWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            return View(categories);
        }


        public IActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {

            bool exists = _unitOfWork.Category.Exists(obj);

            if (exists)
            {
                ModelState.AddModelError("Name", $"Category name {obj.Name} already exists.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Create(obj);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }

            Category obj = _unitOfWork.Category.GetOne(x => x.CategoryId == id);

            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(Category? obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            bool exists = _unitOfWork.Category.Exists(obj);

            if (exists)
            {
                ModelState.AddModelError("Name", $"Category name {obj.Name} already exists.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }

            Category obj = _unitOfWork.Category.GetOne(x => x.CategoryId == id);

            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Category obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
