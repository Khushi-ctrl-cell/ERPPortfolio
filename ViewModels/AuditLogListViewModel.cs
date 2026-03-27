using ERPPortfolio.Models;

namespace ERPPortfolio.ViewModels
{
    public class AuditLogListViewModel
    {
        public PagedResult<AuditLog> PagedLogs { get; set; }
    }
}
