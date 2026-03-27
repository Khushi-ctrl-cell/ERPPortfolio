using System.Web.Mvc;
using ERPPortfolio.Data;
using ERPPortfolio.Services;
using ERPPortfolio.ViewModels;

namespace ERPPortfolio.Controllers
{
    public class AccountController : Controller
    {
        private readonly ERPDbContext _dbContext;
        private readonly AuthenticationService _authenticationService;

        public AccountController()
        {
            _dbContext = new ERPDbContext();
            _authenticationService = new AuthenticationService(_dbContext);
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["CurrentUserId"] != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var currentUser = _authenticationService.ValidateUser(loginModel.Username, loginModel.Password);
            if (currentUser == null)
            {
                ModelState.AddModelError(string.Empty, "The credentials were not recognized. Try admin / Admin@123.");
                return View(loginModel);
            }

            Session["CurrentUserId"] = currentUser.UserId;
            Session["CurrentUsername"] = currentUser.Username;
            Session["CurrentFullName"] = currentUser.FullName;
            TempData["SuccessToast"] = "Welcome back. Your ERP workspace is ready.";
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Clear();
            TempData["SuccessToast"] = "You have been signed out successfully.";
            return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
