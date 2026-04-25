using Expenses_Recorder_App.Models.Application_Models.ApplicationDbContext;
using Expenses_Recorder_App.Models.Application_Models.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace Expenses_Recorder_App.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _context; // Dependency injection of the database context 
        public CategoryController(ApplicationDBContext dbContext)
        {
            _context = dbContext;
        }

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            int? id = HttpContext.Session.GetInt32("UserId");

            if (id == null)
            {
                return RedirectToAction("UserLogin", "User");
            }
            var viewModel = new IndexViewModel() { Categories = _context.Categories.Include(U => U.Users).Where(C => C.UserID == id) };
            return View(viewModel);

        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Create(CategoryCreateEditViewModel CategoryViewModel)
        {
            if (CategoryViewModel == null) return BadRequest();

            int? id = HttpContext.Session.GetInt32("UserId");

            if (id == null)
            {
                return RedirectToAction("UserLogin", "User");
            }
            var newCategory = new Category() { Name = CategoryViewModel.Name, Note = CategoryViewModel.Note , UserID = (int)id };

            _context.Categories.Add(newCategory);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id <= 0) return BadRequest();

            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            var viewModel = new CategoryCreateEditViewModel() { Name = category.Name, Note = category.Note };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CategoryCreateEditViewModel viewModel)
        {
            if (viewModel == null) return BadRequest();

            var currentCategory = _context.Categories.Find(viewModel.Id);
            if (currentCategory == null) return NotFound();

            currentCategory.Name = viewModel.Name;
            currentCategory.Note = viewModel.Note;

            _context.Categories.Update(currentCategory);

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {

            if (id <= 0) return BadRequest();
            var DeletedCategory = _context.Categories.Include(c => c.Expenses).FirstOrDefault(c => c.Id == id);
            if (DeletedCategory?.Expenses == null) return NotFound();

            if (DeletedCategory.Expenses.Any())
            {
                ModelState.AddModelError(string.Empty, "Cannot delete category with associated expenses. Please delete the expenses first.");
                return View(nameof(Delete), DeletedCategory);
            }

            _context.Categories.Remove(DeletedCategory);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
