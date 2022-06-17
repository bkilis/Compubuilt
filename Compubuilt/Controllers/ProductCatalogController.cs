using Compubuilt.Models;
using Compubuilt.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductImageType = Compubuilt.Enums.ProductImageType;

namespace Compubuilt.Controllers
{
    public class ProductCatalogController : Controller
    {
        private readonly compubuiltContext _context;

        public ProductCatalogController(compubuiltContext context)
        {
            _context = context;
        }

        // GET: ProductCatalogController
        public ActionResult Index()
        {
            var productList = _context.Products
                .Include(p => p.ProductImages)
                //.Where(p => p.IsActive == true)
                .ToList();

            var productCatalog = new ProductCatalogViewModel { ProductList = new List<ProductOverview>()};

                foreach (var product in productList)
                {
                    productCatalog.ProductList.Add(new ProductOverview
                    {
                        ProductId = product.ProductId,
                        ProductName = product.Name,
                        Price = product.Price,
                        RatingValue = product.AverageRatingValue,
                        ProductPhotoUrl = product.ProductImages
                            .Where(pi => pi.ProductImageTypeId == (int)ProductImageType.ProductCatalogThumbnail)
                            .Select(pi => pi.Url).FirstOrDefault()
                    });
                }

            return View(productCatalog);
        }

        // GET: ProductCatalogController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductCatalogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductCatalogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductCatalogController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductCatalogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductCatalogController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductCatalogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
