using Compubuilt.Enums;
using Compubuilt.Models;
using Compubuilt.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Compubuilt.ViewComponents
{
    public class NewProductsViewComponent : ViewComponent
    {
        private readonly compubuiltContext _context;

        public NewProductsViewComponent(compubuiltContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var productList = _context.Products
                .Include(p => p.ProductImages)
                .Where(p => p.IsActive == true)
                .OrderByDescending(p => p.CreatedDate)
                .Take(8)
                .ToList();

            var newProducts = new NewProductsViewModel { ProductList = new List<NewProductOverview>() };

            foreach (var product in productList)
            {
                newProducts.ProductList.Add(new NewProductOverview
                {
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    Price = product.Price,
                    RatingValue = product.AverageRatingValue,
                    ProductPhotoUrl = product.ProductImages
                        .Where(pi => pi.ProductImageTypeId == (int)ProductImageTypeEnum.ProductCatalogThumbnail)
                        .Select(pi => pi.Url).FirstOrDefault()
                });
            }

            return View("NewProducts", newProducts);
        }
    }
}
