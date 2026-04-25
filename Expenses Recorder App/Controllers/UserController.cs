using Expenses_Recorder_App.Models.Application_Models.Expenses;
using Expenses_Recorder_App.Models.Application_Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Expenses_Recorder_App.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly Models.Application_Models.ApplicationDbContext.ApplicationDBContext _context;

        public UserController(ILogger<UserController> logger, Models.Application_Models.ApplicationDbContext.ApplicationDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        #region CreateUserPage
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (existingUser != null || model.Password?.Length < 8)
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                ModelState.AddModelError("Password", "Password Should be 8 items or more.");
                return View(model);
            }

            if (model.UserQuestion?.Length < 8 || model.UserAnswer?.Length < 3)
            {
                ModelState.AddModelError("UserQuestion", "Question should be 8 items or more.");
                ModelState.AddModelError("UserAnswer", "Answer should be 3 items or more.");
                return View(model);
            }

            if (model.Salary < 0)
            {
                ModelState.AddModelError("Salary", "Salary should be a positive number.");
                return View(model);
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                UserQuestion = model.UserQuestion ?? "",
                UserQuestionAnswer = BCrypt.Net.BCrypt.HashPassword(model.UserAnswer),
                Salary = model.Salary
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(UserLogin));
        }
        #endregion

        #region UserLoginPage
        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogin(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users.FirstOrDefault(U => U.Email == model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Invalid Email");
                return View(model);
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);


            if (!isPasswordValid)
            {
                ModelState.AddModelError("Password", "Invalid Password");
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", user.Id);

            return RedirectToAction("Index", "Expense");

        }
        #endregion

        #region CheckForgetPassword
        [HttpGet]
        public IActionResult EmailCheckForgetPassword()
        {
            var email = TempData["Email"] as string;
            TempData.Keep("Email");

            if (string.IsNullOrEmpty(email))
                return View();

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return View();

            var model = new QuestionAnswerViewModel
            {
                UserQuestion = $"{user.UserQuestion} ?"
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EmailCheckForgetPassword(QuestionAnswerViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var email = TempData["Email"] as string;
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return View(model);
            }

            if (!BCrypt.Net.BCrypt.Verify(model.UserQuestionAnswer, user.UserQuestionAnswer))
            {
                ModelState.AddModelError("UserQuestionAnswer", "Invalid Answer");
                return View(model);
            }

            TempData.Keep("Email");
            return RedirectToAction(nameof(ResetPassword));
        }

        #endregion

        #region ForgetPassword
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(EnterEmailViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

            if (user is null)
            {
                ModelState.AddModelError("Email", "Invalid Email");
                return View(model);
            }

            TempData["Email"] = user.Email;

            return RedirectToAction(nameof(EmailCheckForgetPassword));
        }
        #endregion


        #region ResetPassword
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (model is null)
            {
                return View(model);
            }

            var email = TempData["Email"] as string;

            if (email is null)
            {
                return View(model);
            }

            var user = _context.Users.FirstOrDefault(U => U.Email == email);

            if (user is null)
            {
                return View(model);
            }

            if(model.Password == null  || model.ConfirmPassword == null)
            {
                ModelState.AddModelError("Password", "Password is required");
                return View(model);
            }

            if (model.Password.Length < 8)
            {
                ModelState.AddModelError("Password", "Password must be at least 8 characters long");
                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match");
                return View(model);
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            _context.SaveChanges();

            return RedirectToAction(nameof(UserLogin));
        }
        #endregion
    }
}
