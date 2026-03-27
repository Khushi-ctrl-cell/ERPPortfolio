using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPPortfolio.Models
{
    public class ProductItem
    {
        public ProductItem()
        {
            ChildItems = new HashSet<ProductItem>();
        }

        [Key]
        public int ItemId { get; set; }

        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Range(0.01, 999999)]
        [Column(TypeName = "decimal")]
        public decimal Weight { get; set; }

        public int? ParentItemId { get; set; }

        [ForeignKey("ParentItemId")]
        public virtual ProductItem ParentItem { get; set; }

        public virtual ICollection<ProductItem> ChildItems { get; set; }
    }
}
