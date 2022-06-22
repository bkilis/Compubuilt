using Compubuilt.Enums;
using Compubuilt.Models;
using Compubuilt.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Compubuilt.ViewComponents
{
    public class ProductFiltersViewComponent : ViewComponent
    {
        private readonly compubuiltContext _context;

        public ProductFiltersViewComponent(compubuiltContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var productCategories = _context.ProductCategories
                .Where(p => p.IsActive == true)
                .ToList();

            var productFiltersModel = new ProductFiltersViewModel();

            foreach (var productCategory in productCategories)
            {
                productFiltersModel.Categories.Add(new ProductFiltersViewModel.ProductCategoryViewModel
                {
                    ProductCategoryId = productCategory.ProductCategoryId,
                    Name = productCategory.Name
                });
            }

            return View("ProductFilters", productFiltersModel);
        }
    }
}
