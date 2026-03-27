using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ERPPortfolio.ViewModels
{
    public class ItemFormViewModel
    {
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Item name is required.")]
        [StringLength(120)]
        [Display(Name = "Item Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        [Range(0.01, 999999, ErrorMessage = "Weight must be greater than zero.")]
        public decimal Weight { get; set; }

        [Display(Name = "Parent Category")]
        public int? ParentItemId { get; set; }

        public IEnumerable<SelectListItem> ParentOptions { get; set; }
    }
}
