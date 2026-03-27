using System;
using System.Text;
using System.Web.Mvc;
using ERPPortfolio.Data;
using ERPPortfolio.Filters;
using ERPPortfolio.Services;
using ERPPortfolio.ViewModels;

namespace ERPPortfolio.Controllers
{
    [SessionAuthorize]
    public class ItemController : Controller
    {
        private const int ItemPageSize = 6;
        private readonly ERPDbContext _dbContext;
        private readonly ProductItemService _productItemService;
        private readonly AuditLogService _auditLogService;

        public ItemController()
        {
            _dbContext = new ERPDbContext();
            _productItemService = new ProductItemService(_dbContext);
            _auditLogService = new AuditLogService(_dbContext);
        }

        public ActionResult Index(string searchTerm, decimal? minWeight, decimal? maxWeight, int page = 1)
        {
            var filterModel = new ItemFilterViewModel
            {
                SearchTerm = searchTerm,
                MinWeight = minWeight,
                MaxWeight = maxWeight
            };

            var itemListViewModel = new ItemListViewModel
            {
                FilterModel = filterModel,
                PagedItems = _productItemService.GetPagedItems(filterModel, page, ItemPageSize)
            };

            return View(itemListViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(_productItemService.BuildItemFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemFormViewModel formModel)
        {
            if (!ModelState.IsValid)
            {
                formModel.ParentOptions = _productItemService.BuildItemFormViewModel().ParentOptions;
                return View(formModel);
            }

            try
            {
                _productItemService.Create(formModel, Convert.ToString(Session["CurrentFullName"]), _auditLogService);
                TempData["SuccessToast"] = "Product item created successfully.";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorToast"] = "We could not create the item. Please review the input and try again.";
                formModel.ParentOptions = _productItemService.BuildItemFormViewModel().ParentOptions;
                return View(formModel);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var formModel = _productItemService.BuildItemFormViewModel(id);
            if (formModel.ItemId == 0)
            {
                return HttpNotFound();
            }

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemFormViewModel formModel)
        {
            if (!ModelState.IsValid)
            {
                formModel.ParentOptions = _productItemService.BuildItemFormViewModel(formModel.ItemId).ParentOptions;
                return View(formModel);
            }

            try
            {
                var updateSucceeded = _productItemService.Update(formModel, Convert.ToString(Session["CurrentFullName"]), _auditLogService);
                if (!updateSucceeded)
                {
                    return HttpNotFound();
                }

                TempData["SuccessToast"] = "Product item updated successfully.";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException exception)
            {
                ModelState.AddModelError("ParentItemId", exception.Message);
                formModel.ParentOptions = _productItemService.BuildItemFormViewModel(formModel.ItemId).ParentOptions;
                return View(formModel);
            }
            catch
            {
                TempData["ErrorToast"] = "The update could not be completed right now.";
                formModel.ParentOptions = _productItemService.BuildItemFormViewModel(formModel.ItemId).ParentOptions;
                return View(formModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var deleteSucceeded = _productItemService.Delete(id, Convert.ToString(Session["CurrentFullName"]), _auditLogService);
            TempData[deleteSucceeded ? "SuccessToast" : "ErrorToast"] = deleteSucceeded
                ? "Product item deleted successfully."
                : "The selected item no longer exists.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Tree()
        {
            return View(_productItemService.BuildItemTree());
        }

        [HttpGet]
        public FileContentResult ExportCsv(string searchTerm, decimal? minWeight, decimal? maxWeight)
        {
            var csvContent = _productItemService.ExportToCsv(new ItemFilterViewModel
            {
                SearchTerm = searchTerm,
                MinWeight = minWeight,
                MaxWeight = maxWeight
            });

            return File(Encoding.UTF8.GetBytes(csvContent), "text/csv", "erp-product-items.csv");
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
