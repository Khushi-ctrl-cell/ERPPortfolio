using System.Collections.Generic;
using ERPPortfolio.Models;

namespace ERPPortfolio.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalItems { get; set; }
        public decimal TotalWeight { get; set; }
        public int TotalParentCategories { get; set; }
        public IList<AuditLog> RecentAuditLogs { get; set; }
    }
}
