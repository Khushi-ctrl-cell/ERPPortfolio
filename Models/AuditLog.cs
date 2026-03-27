using System;
using System.ComponentModel.DataAnnotations;

namespace ERPPortfolio.Models
{
    public class AuditLog
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        [StringLength(50)]
        public string Action { get; set; }

        public DateTime Timestamp { get; set; }

        [Required]
        [StringLength(1000)]
        public string Details { get; set; }
    }
}
