using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Compubuilt.Models;
using Compubuilt.ViewModels;
using ProductImageTypeEnum = Compubuilt.Enums.ProductImageTypeEnum;
using Microsoft.AspNetCore.Authorization;

namespace Compubuilt.Controllers
{
    [Authorize/*(Roles="ShopAdministrator")*/]
    public class ProductsController : Controller
    {
        private readonly compubuiltContext _context;

        public ProductsController(compubuiltContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var compubuiltContext = _context.Products
                .Include(p => p.ProductCategory)
                .Where(p => p.IsActive == true);
            return View(await compubuiltContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Description,Quantity,Price,ProductCategoryId,AverageRatingValue,IsActive,CreatedDate,CreatedBy,LastModifiedDate,LastModifiedBy")] Product product)
        {
            //if (ModelState.IsValid)
            //{
            _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Description,Quantity,Price,ProductCategoryId,AverageRatingValue,IsActive,CreatedDate,CreatedBy,LastModifiedDate,LastModifiedBy")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'compubuiltContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                product.IsActive = false;
                _context.Products.Update(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProductPage(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            var productPage = new ProductPageViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                RatingValue = product.AverageRatingValue,
                ProductCategoryName = product.ProductCategory.Name,
                ProductPhotoThumbnailUrls = product.ProductImages
                    .Where(pi => pi.ProductId == product.ProductId && pi.ProductImageTypeId == (int)ProductImageTypeEnum.ProductGalleryPhoto)
                    .Select(pi => pi.Url).ToList(),
                ProductPhotoUrls = product.ProductImages
                    .Where(pi => pi.ProductId == product.ProductId && pi.ProductImageTypeId == (int)ProductImageTypeEnum.ProductGalleryPhoto)
                    .Select(pi => pi.Url).ToList(),
                InStock = product.Quantity > 0 ? "checked" : "unchecked",
                AddToCartButtonEnabled = product.Quantity > 0 ? "enabled" : "disabled"
            };

            return View(productPage);
        }
    }
}
