using System.Collections.Generic;

namespace ERPPortfolio.ViewModels
{
    public class ProductTreeNodeViewModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public int Level { get; set; }
        public IList<ProductTreeNodeViewModel> ChildNodes { get; set; }
    }
}
