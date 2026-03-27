using System.Web.Mvc;
using ERPPortfolio.Data;
using ERPPortfolio.Filters;
using ERPPortfolio.Services;
using ERPPortfolio.ViewModels;

namespace ERPPortfolio.Controllers
{
    [SessionAuthorize]
    public class AuditLogController : Controller
    {
        private const int AuditPageSize = 10;
        private readonly ERPDbContext _dbContext;
        private readonly AuditLogService _auditLogService;

        public AuditLogController()
        {
            _dbContext = new ERPDbContext();
            _auditLogService = new AuditLogService(_dbContext);
        }

        public ActionResult Index(int page = 1)
        {
            var auditLogListViewModel = new AuditLogListViewModel
            {
                PagedLogs = _auditLogService.GetPagedLogs(page, AuditPageSize)
            };

            return View(auditLogListViewModel);
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
