using ERPPortfolio.Models;

namespace ERPPortfolio.ViewModels
{
    public class ItemListViewModel
    {
        public ItemFilterViewModel FilterModel { get; set; }
        public PagedResult<ProductItem> PagedItems { get; set; }
    }
}
