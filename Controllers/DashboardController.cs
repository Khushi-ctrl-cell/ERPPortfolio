using System.Web.Mvc;
using ERPPortfolio.Data;
using ERPPortfolio.Filters;
using ERPPortfolio.Services;

namespace ERPPortfolio.Controllers
{
    [SessionAuthorize]
    public class DashboardController : Controller
    {
        private readonly ERPDbContext _dbContext;
        private readonly DashboardService _dashboardService;

        public DashboardController()
        {
            _dbContext = new ERPDbContext();
            _dashboardService = new DashboardService(_dbContext);
        }

        public ActionResult Index()
        {
            var dashboardViewModel = _dashboardService.GetDashboardViewModel();
            return View(dashboardViewModel);
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
