using Expenses_Recorder_App.Models.Application_Models.ApplicationDbContext;
using Expenses_Recorder_App.Models.Application_Models.Expenses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Expenses_Recorder_App.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly ApplicationDBContext _context;

        public ExpenseController(ILogger<ExpenseController> logger, ApplicationDBContext dBContext)
        {
            _logger = logger;
            _context = dBContext;
        }


        #region Index
        [HttpGet]
        public IActionResult Index(DateTime? SelectedDate)
        {
            int? id = HttpContext.Session.GetInt32("UserId");

            if (id == null)
            {
                return RedirectToAction("UserLogin", "User");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            var selectedDate = SelectedDate ?? DateTime.Now;

            var model = new ExpenseViewModel
            {
                MonthSalary = user?.Salary ?? 0, // This should ideally come from a user profile or settings
                Category = _context.Categories.Where(C => C.UserID == id).ToList(),
                MonthlyTotal = _context.Expenses.Where(e => e.Date.Month == selectedDate.Month && e.Date.Year == selectedDate.Year && e.UserId == id).Sum(e => e.Amount),
                YearlyTotal = _context.Expenses.Where(e => e.Date.Year == selectedDate.Year && e.UserId == id).Sum(e => e.Amount),
                CategoriesCount = _context.Expenses.Where(e => e.UserId == id).Select(e => e.CategoryId).Distinct().Count(),
                SelectedDate = selectedDate,
                Expenses = _context.Expenses.Where(e => e.Date.Month == selectedDate.Month && e.Date.Year == selectedDate.Year && e.UserId == id).ToList()
            };


            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            int? id = HttpContext.Session.GetInt32("UserId");

            if (id == null)
            {
                return RedirectToAction("UserLogin", "User");
            }
            var model = new ExpenseCreateEditViewModel
            {
                Date = DateTime.Now,
                Categories = _context.Categories.Where(C => C.UserID == id).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ExpenseCreateEditViewModel model)
        {
            int? id = HttpContext.Session.GetInt32("UserId");

            if (id == null)
            {
                return RedirectToAction("UserLogin", "User");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var expense = new Expense()
            {
                CategoryId = model.CategoryId,
                Date = model.Date,
                Amount = model.Amount,
                Note = model.Note,
                UserId = id
            };

            _context.Expenses.Add(expense);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion // needs some modification on the category bar list design

        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            int? Id = HttpContext.Session.GetInt32("UserId");

            if (Id == null)
            {
                return RedirectToAction("UserLogin", "User");
            }
            var expense = _context.Expenses
    .FirstOrDefault(e => e.Id == id && e.UserId == Id); ;
            if (expense == null)
            {
                return NotFound();
            }
            var model = new ExpenseCreateEditViewModel
            {
                Id = expense.Id,
                CategoryId = expense.CategoryId,
                Date = expense.Date,
                Amount = expense.Amount,
                Note = expense.Note,
                Categories = _context.Categories.Where(C => C.UserID == Id).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ExpenseCreateEditViewModel expense)
        {
            if (expense == null) // Form
            {
                return NotFound();
            }

            var existingExpense = _context.Expenses.Find(expense.Id);
            if (existingExpense == null) // Database
            {
                return NotFound();
            }

            existingExpense.CategoryId = expense.CategoryId;
            existingExpense.Date = expense.Date;
            existingExpense.Amount = expense.Amount;
            existingExpense.Note = expense.Note;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var expense = _context.Expenses.Include(e => e.Category).FirstOrDefault(e => e.Id == id);
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            int? Id = HttpContext.Session.GetInt32("UserId");

            if (Id == null)
            {
                return RedirectToAction("UserLogin", "User");
            }
            var expense = _context.Expenses
                .FirstOrDefault(e => e.Id == id && e.UserId == Id); if (expense == null)
            {
                return NotFound();
            }
            _context.Expenses.Remove(expense);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region ChangeSalary
        [HttpGet]
        public IActionResult ChangeSalary()
        {
            int? id = HttpContext.Session.GetInt32("UserId");

            if (id == null)
            {
                return RedirectToAction("UserLogin", "User");
            }
            var model = new SalaryViewModel
            {
                Salary = _context.Users.Where(u => u.Id == id).Select(u => u.Salary).FirstOrDefault()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeSalary(SalaryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            int? id = HttpContext.Session.GetInt32("UserId");

            if (id == null)
            {
                return RedirectToAction("UserLogin", "User");
            }
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Salary = model.Salary;
            _context.SaveChanges();
            TempData.Keep("UserId");
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
